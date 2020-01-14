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
    public sealed class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="binding">Input query bind model</param>
        [HttpGet("projects", Name = "GetProjectsRoute")]
        [ProducesResponseType(typeof(Page<ProjectListView>), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindProjectsBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ProjectListQuery(binding.Offset, binding.Limit), cancellationToken));
        }
        
        /// <summary>
        /// Get a project
        /// </summary>
        /// <param name="id">Project identifier</param>
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