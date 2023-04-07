using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.IssueTimeEntries;
using ProjectManagementSystem.Domain.TimeEntries.Commands;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class IssueTimeEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueTimeEntriesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create time entry for the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <response code="204">Time entry for issue created</response>
    /// <response code="400">Validation failed</response>
    /// <response code="409">Time entry already exists</response>
    /// <response code="422">Issue or user not found</response>
    [HttpPost("/issues/{issueId:guid}/timeEntries")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> CreateTimeEntryForIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromBody] CreateTimeEntryBindingModel model)
    {
        CreateTimeEntryForIssueCommandResultState commandResult = await _mediator.Send(new CreateTimeEntryForIssueCommand
        {
            TimeEntryId = model.Id,
            Hours = model.Hours,
            Description = model.Description,
            DueDate = model.DueDate,
            IssueId = issueId,
            UserId = User.GetId(),
        }, cancellationToken);

        return commandResult switch
        {
            CreateTimeEntryForIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            CreateTimeEntryForIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CreateTimeEntryForIssueCommandResultState.TimeEntryWithSameIdAlreadyExists => Conflict(),
            CreateTimeEntryForIssueCommandResultState.TimeEntryForIssueCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    /// <summary>
    /// Delete time entry from the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <param name="timeEntryId">Time entry identifier</param>
    /// <response code="204">Time entry deleted</response>
    /// <response code="404">Time entry not found</response>
    /// <response code="422">Issue or user not found</response>
    [HttpDelete("/issues/{issueId:guid}/timeEntries/{timeEntryId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> DeleteTimeEntryFromIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromRoute] Guid timeEntryId)
    {
        DeleteTimeEntryFromIssueCommandResultState commandResult = await _mediator.Send(new DeleteTimeEntryFromIssueCommand
        {
            TimeEntryId = timeEntryId,
            IssueId = issueId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResult switch
        {
            DeleteTimeEntryFromIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            DeleteTimeEntryFromIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            DeleteTimeEntryFromIssueCommandResultState.TimeEntryNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound.Title, ErrorCode.TimeEntryNotFound.Detail,
                HttpContext.Request.Path),
            DeleteTimeEntryFromIssueCommandResultState.TimeEntryFromEntryRemoved => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}