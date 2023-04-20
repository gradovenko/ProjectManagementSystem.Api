using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.Issues;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Issues controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class IssuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssuesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Get the list of issues
    /// </summary>
    /// <param name="model">Input model</param>
    [HttpGet("/issues", Name = "GetIssueList")]
    [ProducesResponseType(typeof(Page<IssueListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromQuery] GetIssueListBindingModel model)
    {
        return Ok(await _mediator.Send(new IssueListQuery(model.Offset, model.Limit), cancellationToken));
    }
    
    /// <summary>
    /// Get the issue
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <response code="200">Project view model</response>
    [HttpGet("/issues/{id:guid}", Name = "GetIssue")]
    [ProducesResponseType(typeof(IssueViewModel), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromRoute] Guid id)
    {
        IssueViewModel? viewModel = await _mediator.Send(new IssueQuery(id), cancellationToken);

        if (viewModel == null)
            return this.StatusCode(HttpStatusCode.NotFound, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path);

        return Ok(viewModel);
    }

    /// <summary>
    /// Update the issue
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <response code="204">Issue updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Issue not found </response>
    /// <response code="422">User not found</response>
    [HttpPut("/issues/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromBody] UpdateIssueBindingModel model)
    {
        UpdateIssueCommandResultState commandResult = await _mediator.Send(new UpdateIssueCommand
        {
            IssueId = id,
            Title = model.Title,
            Description = model.Description,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResult switch
        {
            UpdateIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            UpdateIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            UpdateIssueCommandResultState.IssueUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Close the issue
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <response code="204">Issue closed</response>
    /// <response code="404">Issue not found </response>
    /// <response code="422">User not found</response>
    [HttpPut("/issues/{id:guid}/close")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Close(
        CancellationToken cancellationToken,
        [FromRoute] Guid id)
    {
        CloseIssueCommandResultState commandResult = await _mediator.Send(new CloseIssueCommand
        {
            IssueId = id,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResult switch
        {
            CloseIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CloseIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            CloseIssueCommandResultState.IssueClosed => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Reopen the issue
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <response code="204">Issue reopened</response>
    /// <response code="404">Issue not found </response>
    /// <response code="422">User not found</response>
    [HttpPut("/issues/{id:guid}/reopen")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Reopen(
        CancellationToken cancellationToken,
        [FromRoute] Guid id)
    {
        ReopenIssueCommandResultState commandResult = await _mediator.Send(new ReopenIssueCommand
        {
            IssueId = id,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResult switch
        {
            ReopenIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            ReopenIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            ReopenIssueCommandResultState.IssueReopened => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}