using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.CreateProjects;
using ProjectManagementSystem.Queries.Admin.Projects;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.Projects;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="model"></param>
        /// <param name="projectRepository"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("admin/projects")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateProjectBindModel model,
            [FromServices] IProjectRepository projectRepository)
        {
            var project = await projectRepository.Get(model.Id, cancellationToken);

            if (project != null)
                if (!project.Name.Equals(model.Name) ||
                    !project.Description.Equals(model.Description))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.ProjectAlreadyExists,
                        "Project already exists with other parameters");

            project = new Project(model.Id, model.Name, model.Description, model.IsPublic);

            await projectRepository.Save(project);

            return CreatedAtRoute("GetProjectAdminRoute", new {id = project.Id}, null);
        }

        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="model"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet("admin/projects", Name = "GetProjectsAdminRoute")]
        [ProducesResponseType(typeof(ShortProjectView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryProjectBindModel model,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ProjectsQuery(model.Offset, model.Limit), cancellationToken));
        }

        /// <summary>
        /// Get a project
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
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