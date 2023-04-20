using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.IssueComments;
using ProjectManagementSystem.Domain.Comments.Commands;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.IssueComments;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Issue comments controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class IssueCommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueCommentsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create the comment for the issue
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <param name="model">Input model</param>
    /// <response code="400">Input model validation failed</response>
    [HttpPost("/issues/{id:guid}/comments")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> CreateCommentForComment(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromBody] CreateCommentBindingModel model)
    {
        CreateCommentForIssueCommandResultState commandResult = await _mediator.Send(new CreateCommentForIssueCommand
        {
            CommentId = model.Id,
            Content = model.Content,
            AuthorId = User.GetId(),
            IssueId = id,
            ParentCommentId = model.ParentCommentId
        }, cancellationToken);

        return commandResult switch
        {
            CreateCommentForIssueCommandResultState.AuthorNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.AuthorNotFound.Title, ErrorCode.AuthorNotFound.Detail,
                HttpContext.Request.Path),
            CreateCommentForIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path),
            CreateCommentForIssueCommandResultState.ParentCommentNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.ParentCommentNotFound.Title, ErrorCode.ParentCommentNotFound.Detail,
                HttpContext.Request.Path),
            CreateCommentForIssueCommandResultState.ParentCommentAlreadyChildCommentOfAnotherComment => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.ParentCommentAlreadyChildCommentOfAnotherComment.Title, ErrorCode.ParentCommentAlreadyChildCommentOfAnotherComment.Detail,
                HttpContext.Request.Path),
            CreateCommentForIssueCommandResultState.CommentWithSameIdButOtherParamsAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.CommentWithSameIdButOtherParamsAlreadyExists.Title, ErrorCode.CommentWithSameIdButOtherParamsAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateCommentForIssueCommandResultState.CommentCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of issue comments
    /// </summary>
    /// <param name="id">Issue identifier</param>
    /// <param name="model">Input model</param>
    [HttpGet("/issues/{id:guid}/comments", Name = "GetIssueCommentList")]
    [ProducesResponseType(typeof(Page<CommentListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromQuery] GetCommentListBindingModel model)
    {
        return Ok(await _mediator.Send(new CommentListQuery(id, model.Offset, model.Limit), cancellationToken));
    }
}