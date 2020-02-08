using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.Projects;
using ProjectManagementSystem.Domain.Admin.Projects;
using ProjectManagementSystem.Queries.Admin.Projects;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="400">Validation failed</response>
        [HttpPost("admin/projects")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateProjectBinding binding,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] ITrackerRepository trackerRepository)
        {
            var project = await projectRepository.Get(binding.Id, cancellationToken);

            if (project != null)
                if (!project.Name.Equals(binding.Name) ||
                    !project.Description.Equals(binding.Description))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.ProjectAlreadyExists,
                        "Project already exists with other parameters");

            project = new Project(binding.Id, binding.Name, binding.Description, binding.IsPrivate);
            
            foreach (var trackerId in binding.Trackers)
            {
                var tracker = await trackerRepository.Get(trackerId, cancellationToken);
                
                if (tracker == null)
                    throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");
                
                var projectTracker = new ProjectTracker(binding.Id, tracker.Id);
                
                project.AddProjectTracker(projectTracker);
            }

            await projectRepository.Save(project);

            return CreatedAtRoute("GetProjectAdminRoute", new {id = project.Id}, null);
        }

        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="binding">Input model</param>
        [HttpGet("admin/projects", Name = "GetProjectsAdminRoute")]
        [ProducesResponseType(typeof(ProjectView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindProjectsBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ProjectListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get the project
        /// </summary>
        /// <param name="id">Project identifier</param>
        [HttpGet("admin/projects/{id}", Name = "GetProjectAdminRoute")]
        [ProducesResponseType(typeof(ProjectListItemView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var project = await mediator.Send(new ProjectQuery(id), cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            return Ok(project);
        }
    }
}