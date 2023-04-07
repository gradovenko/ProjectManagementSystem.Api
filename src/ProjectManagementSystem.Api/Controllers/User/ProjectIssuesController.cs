using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.ProjectIssues;
using ProjectManagementSystem.Domain.Issues.Commands;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.ProjectIssues;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class ProjectIssuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectIssuesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create the project issue
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <param name="model">Input model</param>
    /// <response code="400">Validation failed</response>
    [HttpPost("/projects/{id:guid}/issues")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> CreateIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromBody] CreateIssueBindingModel model)
    {
        CreateIssueCommandResultState commandResult = await _mediator.Send(new CreateIssueCommand
        {
            IssueId = model.Id,
            Title = model.Title,
            Description = model.Description,
            ProjectId = id,
            AuthorId = model.AuthorId,
            AssigneeIds = model.AssigneeIds,
            LabelIds = model.LabelIds,
            DueDate = model.DueDate
        }, cancellationToken);

        return commandResult switch
        {
            CreateIssueCommandResultState.IssueAlreadyExists => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CreateIssueCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CreateIssueCommandResultState.AuthorNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CreateIssueCommandResultState.AssigneeNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            CreateIssueCommandResultState.IssueCreated => CreatedAtRoute(nameof(Get), new { id = model.Id }, null),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of project issues
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <param name="model">Input model</param>
    [HttpGet("/projects/{id:guid}/issues", Name = "GetProjectIssueList")]
    [ProducesResponseType(typeof(PageViewModel<ProjectIssueListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromQuery] GetIssueListBindingModel model)
    {
        return Ok(await _mediator.Send(new ProjectIssueListQuery(id, model.Offset, model.Limit), cancellationToken));
    }

    /// <summary>
    /// Get the project issue
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <param name="issueId">Issue identifier</param>
    /// <response code="200">Issue view model</response>
    [HttpGet("/projects/{projectId:guid}/issues/{issueId:guid}", Name = "GetProjectIssue")]
    [ProducesResponseType(typeof(ProjectIssueViewModel), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId,
        [FromRoute] Guid issueId)
    {
        ProjectIssueViewModel? viewModel = await _mediator.Send(new ProjectIssueQuery(projectId, issueId), cancellationToken);

        if (viewModel == null)
            return this.StatusCode(HttpStatusCode.NotFound, ErrorCode.IssueNotFound.Title, ErrorCode.IssueNotFound.Detail,
                HttpContext.Request.Path);

        return Ok(viewModel);
    }

    // /// <summary>
    // /// Get the project issue
    // /// </summary>
    // /// <param name="projectId">Project identifier</param>
    // /// <param name="issueId">Issue identifier</param>
    // [HttpGet("/projects/{projectId:guid}/issues/{issueId:guid}", Name = "GetProjectIssue")]
    // [ProducesResponseType(typeof(IssueView), 200)]
    // [ProducesResponseType(typeof(ProblemDetails), 404)]
    // public async Task<IActionResult> Get(
    //     CancellationToken cancellationToken,
    //     [FromRoute] Guid projectId,
    //     [FromRoute] Guid issueId)
    // {
    //     IssueView? issue = await _mediator.Send(new IssueQuery(projectId, issueId), cancellationToken);
    //
    //     if (issue == null)
    //         return this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path);
    //
    //     return Ok(issue);
    // }
    //
    // /// <summary>
    // /// Update the issue
    // /// </summary>
    // /// <param name="projectId">Project identifier</param>
    // /// <param name="issueId">Issue identifier</param>
    // [HttpPut("/projects/{projectId:guid}/issues/{issueId:guid}")]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(typeof(ProblemDetails), 404)]
    // public async Task<IActionResult> Update(
    //     CancellationToken cancellationToken,
    //     [FromRoute] Guid projectId,
    //     [FromRoute] Guid issueId,
    //     [FromBody] UpdateIssueBindingModel model)
    // {
    //     UpdateProjectLabelCommandResultState commandResult = await _mediator.Send(new UpdateProjectLabelCommand
    //     {
    //         LabelId = labelId,
    //         ProjectId = projectId,
    //         Title = model.Title,
    //         Description = model.Description,
    //         BackgroundColor = model.BackgroundColor
    //     }, cancellationToken);
    //
    //     return commandResult switch
    //     {
    //         UpdateProjectLabelCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         UpdateProjectLabelCommandResultState.LabelWithSameTitleAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         UpdateProjectLabelCommandResultState.LabelWithSameIdButOtherParamsAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         UpdateProjectLabelCommandResultState.LabelUpdated => NoContent(),
    //         _ => throw new ArgumentOutOfRangeException()
    //     };
    // }
    //
    // /// <summary>
    // /// Close the issue
    // /// </summary>
    // /// <param name="projectId">Project identifier</param>
    // /// <param name="issueId">Issue identifier</param>
    // [HttpPut("/projects/{projectId:guid}/issues/{issueId:guid}/close")]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(typeof(ProblemDetails), 404)]
    // public async Task<IActionResult> Close(
    //     CancellationToken cancellationToken,
    //     [FromRoute] Guid projectId,
    //     [FromRoute] Guid issueId)
    // {
    //     CloseIssueCommandResultState commandResult = await _mediator.Send(new CloseIssueCommand
    //     {
    //         IssueId = issueId,
    //         ProjectId = projectId
    //     }, cancellationToken);
    //
    //     return commandResult switch
    //     {
    //         CloseIssueCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.FailedDependency, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         CloseIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.FailedDependency, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         CloseIssueCommandResultState.IssueClosed => NoContent(),
    //         _ => throw new ArgumentOutOfRangeException()
    //     };
    // }
    //
    // /// <summary>
    // /// Reopen the issue
    // /// </summary>
    // /// <param name="projectId">Project identifier</param>
    // /// <param name="issueId">Issue identifier</param>
    // [HttpPut("/projects/{projectId:guid}/issues/{issueId:guid}/reopen")]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(typeof(ProblemDetails), 404)]
    // public async Task<IActionResult> Reopen(
    //     CancellationToken cancellationToken,
    //     [FromRoute] Guid projectId,
    //     [FromRoute] Guid issueId)
    // {
    //     ReopenIssueCommandResultState commandResult = await _mediator.Send(new ReopenIssueCommand
    //     {
    //         IssueId = issueId,
    //         ProjectId = projectId
    //     }, cancellationToken);
    //
    //     return commandResult switch
    //     {
    //         ReopenIssueCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.FailedDependency, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         ReopenIssueCommandResultState.IssueNotFound => this.StatusCode(HttpStatusCode.FailedDependency, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
    //             HttpContext.Request.Path),
    //         ReopenIssueCommandResultState.IssueReopened => NoContent(),
    //         _ => throw new ArgumentOutOfRangeException()
    //     };
    // }
}