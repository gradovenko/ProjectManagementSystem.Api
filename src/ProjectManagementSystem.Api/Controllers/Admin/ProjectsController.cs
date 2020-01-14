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
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="model">Input bind model</param>
        [HttpPost("admin/projects")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateProjectBinding model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] ITrackerRepository trackerRepository)
        {
            var project = await projectRepository.Get(model.Id, cancellationToken);

            if (project != null)
                if (!project.Name.Equals(model.Name) ||
                    !project.Description.Equals(model.Description))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.ProjectAlreadyExists,
                        "Project already exists with other parameters");

            project = new Project(model.Id, model.Name, model.Description, model.IsPrivate);
            
            foreach (var trackerId in model.Trackers)
            {
                var tracker = await trackerRepository.Get(trackerId, cancellationToken);
                
                if (tracker == null)
                    throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");
                
                var projectTracker = new ProjectTracker(model.Id, tracker.Id);
                
                project.AddProjectTracker(projectTracker);
            }

            await projectRepository.Save(project);

            return CreatedAtRoute("GetProjectAdminRoute", new {id = project.Id}, null);
        }

        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="binding">Input query model</param>
        [HttpGet("admin/projects", Name = "GetProjectsAdminRoute")]
        [ProducesResponseType(typeof(ShortProjectView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindProjectsBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ProjectsQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get a project
        /// </summary>
        /// <param name="id">Project identifier</param>
        [HttpGet("admin/projects/{id}", Name = "GetProjectAdminRoute")]
        [ProducesResponseType(typeof(FullProjectView), 200)]
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