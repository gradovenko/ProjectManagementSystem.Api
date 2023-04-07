using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Projects;
using ProjectManagementSystem.Domain.Projects.Commands;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.Projects;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create the project
    /// </summary>
    /// <param name="model">Input model</param>
    /// <response code="201">Project created</response>
    /// <response code="400">Validation failed</response>
    /// <response code="409">Label with same title and/or path and/or id but other params already exists</response>
    /// <response code="422">Project not found</response>
    [HttpPost("/projects")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        [FromBody] CreateProjectBindingModel model)
    {
        CreateProjectCommandResultState commandResultState = await _mediator.Send(new CreateProjectCommand
        {
            ProjectId = model.Id,
            Name = model.Name,
            Description = model.Description,
            Path = model.Path,
            Visibility = model.Visibility
        }, cancellationToken);

        return commandResultState switch
        {
            CreateProjectCommandResultState.ProjectWithSameNameAlreadyExists => 
                this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ProjectWithSameNameAlreadyExists.Title, ErrorCode.ProjectWithSameNameAlreadyExists.Detail, HttpContext.Request.Path),
            CreateProjectCommandResultState.ProjectWithSamePathAlreadyExists => 
                this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ProjectWithSamePathAlreadyExists.Title, ErrorCode.ProjectWithSamePathAlreadyExists.Detail, HttpContext.Request.Path),
            CreateProjectCommandResultState.ProjectWithSameIdButOtherParamsAlreadyExists => 
                this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ProjectWithSameIdButOtherParamsAlreadyExists.Title, ErrorCode.ProjectWithSameIdButOtherParamsAlreadyExists.Detail, HttpContext.Request.Path),
            CreateProjectCommandResultState.ProjectCreated => CreatedAtRoute("GetProject", new { id = model.Id }, null),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of projects
    /// </summary>
    /// <param name="binding">Input model</param>
    /// <response code="200">Project list page</response>
    [HttpGet("/projects", Name = "GetProjectList")]
    [ProducesResponseType(typeof(PageViewModel<ProjectListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromQuery] GetProjectListBindingModel binding)
    {
        return Ok(await _mediator.Send(new ProjectListQuery(binding.Offset, binding.Limit), cancellationToken));
    }

    /// <summary>
    /// Get the project
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <response code="200">Project view model</response>
    /// <response code="404">Project not found</response>
    [HttpGet("/projects/{id:guid}", Name = "GetProject")]
    [ProducesResponseType(typeof(ProjectViewModel), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromRoute] Guid id)
    {
        ProjectViewModel? project = await _mediator.Send(new ProjectQuery(id), cancellationToken);

        if (project == null)
            return this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound.Title, ErrorCode.ProjectNotFound.Detail, HttpContext.Request.Path);

        return Ok(project);
    }

    /// <summary>
    /// Update the project
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <response code="204">Project updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Project not found</response>
    /// <response code="409">Project with same title and/or path already exists</response>
    [HttpPut("/projects/{projectId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectBindingModel model)
    {
        UpdateProjectCommandResultState commandResultState = await _mediator.Send(new UpdateProjectCommand
        {
            ProjectId = projectId,
            Name = model.Name,
            Description = model.Description,
            Path = model.Path,
            Visibility = model.Visibility
        }, cancellationToken);

        return commandResultState switch
        {
            UpdateProjectCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound.Title, ErrorCode.ProjectNotFound.Detail,
                HttpContext.Request.Path),
            UpdateProjectCommandResultState.ProjectWithSameNameAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ProjectWithSameNameAlreadyExists.Title, ErrorCode.ProjectWithSameNameAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateProjectCommandResultState.ProjectWithSamePathAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.ProjectWithSamePathAlreadyExists.Title, ErrorCode.ProjectWithSamePathAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateProjectCommandResultState.ProjectUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Delete the project
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <response code="204">Project deleted</response>
    /// <response code="404">Project not found</response>
    [HttpDelete("/projects/{projectId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId)
    {
        DeleteProjectCommandResultState commandResultState = await _mediator.Send(new DeleteProjectCommand
        {
            ProjectId = projectId
        }, cancellationToken);

        return commandResultState switch
        {
            DeleteProjectCommandResultState.ProjectNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            DeleteProjectCommandResultState.ProjectDeleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}