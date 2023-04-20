using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Domain.Reactions.Commands;
using ProjectManagementSystem.Queries;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Reactions controller
/// </summary>
/// <response code="401">Unauthorized</response>
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class ReactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReactionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #region User

    /// <summary>
    /// Get the list of reactions as user
    /// </summary>
    /// <response code="200">Reaction list</response>
    [Authorize(Roles = UserRole.AdminAndUser)]
    [HttpGet("/reactions", Name = "GetUserReactionList")]
    [ProducesResponseType(typeof(IEnumerable<Queries.User.Reactions.ReactionListItemViewModel>), 200)]
    public async Task<IActionResult> GetUserList(
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new Queries.User.Reactions.ReactionListQuery(), cancellationToken));
    }

    #endregion

    #region Admin

    /// <summary>
    /// Create the reaction as admin
    /// </summary>
    /// <param name="model">Input model</param>
    /// <response code="201">Reaction created</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="409">Reaction with same unicode already exists</response>
    [Authorize(Roles = UserRole.Admin)]
    [HttpPost("/admin/reactions")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        [FromBody] Models.Admin.Reactions.CreateReactionBindingModel model)
    {
        CreateReactionCommandResultState commandResultState = await _mediator.Send(new CreateReactionCommand
        {
            ReactionId = model.Id,
            Emoji = model.Emoji,
            Name = model.Name,
            Category = model.Category
        }, cancellationToken);

        return commandResultState switch
        {
            CreateReactionCommandResultState.ReactionWithSameEmojiAlreadyExists => this.StatusCode(
                HttpStatusCode.Conflict, ErrorCode.ReactionWithSameEmojiAlreadyExists.Title,
                ErrorCode.ReactionWithSameEmojiAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateReactionCommandResultState.ReactionWithSameIdButOtherParamsAlreadyExists => this.StatusCode(
                HttpStatusCode.Conflict, ErrorCode.ReactionWithSameIdButDifferentParamsAlreadyExists.Title,
                ErrorCode.ReactionWithSameIdButDifferentParamsAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateReactionCommandResultState.ReactionCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of reactions as admin
    /// </summary>
    /// <response code="200">Reaction list</response>
    [Authorize(Roles = UserRole.Admin)]
    [HttpGet("/admin/reactions", Name = "GetAdminReactionList")]
    [ProducesResponseType(typeof(Page<Queries.Admin.Reactions.ReactionListItemViewModel>), 200)]
    public async Task<IActionResult> GetAdminList(
        CancellationToken cancellationToken,
        [FromQuery] Models.Admin.Reactions.GetReactionListBindingModel model)
    {
        return Ok(await _mediator.Send(new Queries.Admin.Reactions.ReactionListQuery(model.Offset, model.Limit), cancellationToken));
    }

    /// <summary>
    /// Update the reaction as admin
    /// </summary>
    /// <param name="reactionId">Reaction identifier</param>
    /// <response code="204">Reaction updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Reaction not found</response>
    /// <response code="409">Reaction with same emoji already exists</response>
    [Authorize(Roles = UserRole.Admin)]
    [HttpPut("/admin/reactions/{reactionId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid reactionId,
        [FromBody] Models.Admin.Reactions.UpdateReactionBindingModel model)
    {
        UpdateReactionCommandResultState commandResultState = await _mediator.Send(new UpdateReactionCommand
        {
            ReactionId = reactionId,
            Emoji = model.Emoji,
            Name = model.Name,
            Category = model.Category
        }, cancellationToken);

        return commandResultState switch
        {
            UpdateReactionCommandResultState.ReactionNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ReactionNotFound.Title, ErrorCode.ReactionNotFound.Detail,
                HttpContext.Request.Path),
            UpdateReactionCommandResultState.ReactionWithSameEmojiAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ReactionWithSameEmojiAlreadyExists.Title, ErrorCode.ReactionWithSameEmojiAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateReactionCommandResultState.ReactionUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}