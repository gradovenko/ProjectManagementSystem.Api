using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.IssueLabels;
using ProjectManagementSystem.Domain.Issues.Commands;
using ProjectManagementSystem.Queries.User.IssueLabels;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Issue labels controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class IssueLabelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueLabelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    /// <summary>
    /// Get the list of issue labels
    /// </summary>
    /// <response code="200">Issue label list</response>
    [HttpGet("/issues/{issueId:guid}/labels", Name = "GetIssueLabelList")]
    [ProducesResponseType(typeof(IEnumerable<LabelListItemViewModel>), 200)]
    public async Task<IActionResult> GetIssueLabelList(
        [FromRoute] Guid issueId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new LabelListQuery(issueId), cancellationToken));
    }

    /// <summary>
    /// Add the label to the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <param name="model">Input binding model</param>
    /// <response code="204">Label to the issue added</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Label not found</response>
    /// <response code="422">Issue or user not found</response>
    [HttpPost("/issues/{issueId:guid}/labels")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> AddLabelToIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromBody] AddLabelToIssueBindingModel model)
    {
        AddLabelToIssueCommandResultState commandResultState = await _mediator.Send(new AddLabelToIssueCommand
        {
            LabelId = model.Id,
            IssueId = issueId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            AddLabelToIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            AddLabelToIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            AddLabelToIssueCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            AddLabelToIssueCommandResultState.LabelToIssueAdded => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Remove the label from the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <param name="labelId">Label identifier</param>
    /// <response code="204">Label from the issue deleted</response>
    /// <response code="404">Label not found</response>
    /// <response code="422">Issue or user not found</response>
    [HttpDelete("/issues/{issueId:guid}/labels/{labelId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> RemoveLabelFromIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromRoute] Guid labelId)
    {
        RemoveLabelFromIssueCommandResultState commandResultState = await _mediator.Send(new RemoveLabelFromIssueCommand
        {
            LabelId = labelId,
            IssueId = issueId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            RemoveLabelFromIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            RemoveLabelFromIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            RemoveLabelFromIssueCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            RemoveLabelFromIssueCommandResultState.LabelFromIssueRemoved => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}