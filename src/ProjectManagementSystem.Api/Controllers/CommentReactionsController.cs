using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.CommentReactions;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Comment reactions controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class CommentReactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentReactionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    /// <summary>
    /// Get the list of comment reactions
    /// </summary>
    /// <response code="200">Comment reaction list</response>
    [HttpGet("/comments/{commentId:guid}/reactions", Name = "GetCommentReactionList")]
    [ProducesResponseType(typeof(IEnumerable<Queries.User.CommentReactions.ReactionListItemViewModel>), 200)]
    public async Task<IActionResult> GetCommentReactionList(
        [FromRoute] Guid commentId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new Queries.User.CommentReactions.ReactionListQuery(commentId), cancellationToken));
    }

    /// <summary>
    /// Add the reaction to the comment
    /// </summary>
    /// <param name="commentId">Comment identifier</param>
    /// <param name="model">Input binding model</param>
    /// <response code="204">Reaction to the comment added</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="400">Reaction not found</response>
    /// <response code="422">Comment or user not found</response>
    [HttpPost("/comments/{commentId:guid}/reactions")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> AddReactionToComment(
        CancellationToken cancellationToken,
        [FromRoute] Guid commentId,
        [FromBody] AddReactionToCommentBindingModel model)
    {
        AddReactionToCommentCommandResultState commandResultState = await _mediator.Send(new AddReactionToCommentCommand
        {
            ReactionId = model.Id,
            CommentId = commentId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            AddReactionToCommentCommandResultState.CommentNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.CommentNotFound.Title, ErrorCode.CommentNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToCommentCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToCommentCommandResultState.ReactionNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ReactionNotFound.Title, ErrorCode.ReactionNotFound.Detail,
                HttpContext.Request.Path),
            AddReactionToCommentCommandResultState.ReactionToCommentAdded => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Remove the reaction from the comment
    /// </summary>
    /// <param name="commentId">Comment identifier</param>
    /// <param name="reactionId">Reaction identifier</param>
    /// <response code="204">Reaction to the comment added</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="400">Reaction not found</response>
    /// <response code="422">Comment or user not found</response>
    [HttpDelete("/comments/{commentId:guid}/reactions/{reactionId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> RemoveReactionFromComment(
        CancellationToken cancellationToken,
        [FromRoute] Guid commentId,
        [FromRoute] Guid reactionId)
    {
        RemoveReactionFromCommentCommandResultState commandResultState = await _mediator.Send(new RemoveReactionFromCommentCommand
        {
            ReactionId = reactionId,
            CommentId = commentId,
            UserId = User.GetId()
        }, cancellationToken);

        return commandResultState switch
        {
            RemoveReactionFromCommentCommandResultState.CommentNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.CommentNotFound.Title, ErrorCode.CommentNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromCommentCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromCommentCommandResultState.ReactionNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ReactionNotFound.Title, ErrorCode.ReactionNotFound.Detail,
                HttpContext.Request.Path),
            RemoveReactionFromCommentCommandResultState.ReactionFromCommentRemoved => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}