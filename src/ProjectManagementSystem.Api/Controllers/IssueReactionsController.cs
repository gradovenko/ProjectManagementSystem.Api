using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.IssueReactions;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Issue reactions controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class IssueReactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueReactionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    /// <summary>
    /// Get list of issue reactions
    /// </summary>
    /// <response code="200">Issue reaction list</response>
    [HttpGet("/issues/{issueId:guid}/reactions", Name = "GetIssueReactionList")]
    [ProducesResponseType(typeof(IEnumerable<Queries.User.IssueReactions.ReactionListItemViewModel>), 200)]
    public async Task<IActionResult> GetIssueReactionList(
        [FromRoute] Guid issueId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new Queries.User.IssueReactions.ReactionListQuery(issueId), cancellationToken));
    }

    /// <summary>
    /// Add reaction to the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <param name="model">Input binding model</param>
    /// <response code="204">Reaction to the issue added</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="400">Reaction not found</response>
    /// <response code="422">Issue or user not found</response>
    [HttpPost("/issues/{issueId:guid}/reactions")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> AddReactionToIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromBody] AddReactionToIssueBindingModel model)
    {
        AddReactionToIssueCommandResultState commandResultState = await _mediator.Send(new AddReactionToIssueCommand
        {
            ReactionId = model.Id,
            IssueId = issueId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            AddReactionToIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToIssueCommandResultState.ReactionNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ReactionNotFound.Title, ErrorCode.ReactionNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToIssueCommandResultState.ReactionToIssueAdded => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Remove reaction from the issue
    /// </summary>
    /// <param name="issueId">Issue identifier</param>
    /// <param name="reactionId">Reaction identifier</param>
    [HttpDelete("/issues/{issueId:guid}/reactions/{reactionId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> RemoveReactionFromIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid issueId,
        [FromRoute] Guid reactionId)
    {
        RemoveReactionFromIssueCommandResultState commandResultState = await _mediator.Send(new RemoveReactionFromIssueCommand
        {
            ReactionId = reactionId,
            IssueId = issueId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            RemoveReactionFromIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromIssueCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromIssueCommandResultState.ReactionNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound.Title, ErrorCode.TimeEntryNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromIssueCommandResultState.ReactionFromIssueRemoved => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}