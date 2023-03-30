using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;
using ProjectManagementSystem.Queries.User.Labels;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class ProjectLabelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectLabelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create the project label
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <param name="model">Input model</param>
    /// <response code="201">Label created</response>
    /// <response code="400">Validation failed</response>
    /// <response code="409">Label with same title already exists</response>
    /// <response code="422">Project not found</response>
    [HttpPost("/projects/{id:guid}/labels")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromBody] CreateProjectLabelBindingModel model)
    {
        CreateProjectLabelCommandResultState commandResultState = await _mediator.Send(new CreateProjectLabelCommand
        {
            LabelId = model.Id,
            ProjectId = id,
            Title = model.Title,
            Description = model.Description,
            BackgroundColor = model.BackgroundColor
        }, cancellationToken);

        return commandResultState switch
        {
            CreateProjectLabelCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.ProjectNotFound.Title, ErrorCode.ProjectNotFound.Detail,
                HttpContext.Request.Path),
            CreateProjectLabelCommandResultState.LabelWithSameTitleAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameTitleAlreadyExists.Title, ErrorCode.LabelWithSameTitleAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateProjectLabelCommandResultState.LabelWithSameIdButOtherParamsAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameIdButOtherParamsAlreadyExists.Title, ErrorCode.LabelWithSameIdButOtherParamsAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateProjectLabelCommandResultState.LabelCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of project labels
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <response code="200">The list of project labels</response>
    [HttpGet("/projects/{projectId:guid}/labels", Name = "GetProjectLabelList")]
    [ProducesResponseType(typeof(IEnumerable<LabelListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId)
    {
        return Ok(await _mediator.Send(new LabelListQuery(projectId), cancellationToken));
    }

    /// <summary>
    /// Update the project label
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <param name="labelId">Label identifier</param>
    /// <response code="204">Label updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Label not found</response>
    /// <response code="409">Label with same title already exists</response>
    /// <response code="422">Project not found</response>
    [HttpPut("/projects/{projectId:guid}/labels/{labelId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId,
        [FromRoute] Guid labelId,
        [FromBody] UpdateLabelBindingModel model)
    {
        UpdateProjectLabelCommandResultState commandResultState = await _mediator.Send(new UpdateProjectLabelCommand
        {
            LabelId = labelId,
            ProjectId = projectId,
            Title = model.Title,
            Description = model.Description,
            BackgroundColor = model.BackgroundColor
        }, cancellationToken);

        return commandResultState switch
        {
            UpdateProjectLabelCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.ProjectNotFound.Title, ErrorCode.ProjectNotFound.Detail,
                HttpContext.Request.Path),
            UpdateProjectLabelCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            UpdateProjectLabelCommandResultState.LabelWithSameTitleAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameTitleAlreadyExists.Title, ErrorCode.LabelWithSameTitleAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateProjectLabelCommandResultState.LabelUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Delete the project label
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <param name="labelId">Label identifier</param>
    /// <response code="204">Label deleted</response>
    /// <response code="404">Label not found</response>
    /// <response code="422">Project not found</response>
    [HttpDelete("/projects/{projectId:guid}/labels/{labelId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId,
        [FromRoute] Guid labelId)
    {
        DeleteProjectLabelCommandResultState commandResultState = await _mediator.Send(new DeleteProjectLabelCommand
        {
            LabelId = labelId,
            ProjectId = projectId
        }, cancellationToken);

        return commandResultState switch
        {
            DeleteProjectLabelCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.ProjectNotFound.Title, ErrorCode.ProjectNotFound.Detail,
                HttpContext.Request.Path),
            DeleteProjectLabelCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            DeleteProjectLabelCommandResultState.LabelDeleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}