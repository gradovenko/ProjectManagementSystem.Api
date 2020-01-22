using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.User.Projects;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.Projects;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Project list page</response>
        [HttpGet("projects", Name = "GetProjectsRoute")]
        [ProducesResponseType(typeof(Page<ProjectListItemView>), 200)]
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
        /// <response code="200">Project</response>
        /// <response code="404">Project not found</response>
        [HttpGet("projects/{id}", Name = "GetProjectRoute")]
        [ProducesResponseType(typeof(ProjectView), 200)]
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