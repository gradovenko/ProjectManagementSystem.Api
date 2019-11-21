using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries.User.ProjectTimeEntries;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class ProjectTimeEntriesController : ControllerBase
    {
        [HttpGet("projects/{projectId}/timeEntries", Name = "GetProjectTimeEntriesRoute")]
        [ProducesResponseType(typeof(TimeEntryListViewModel), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromQuery] QueryPageBindModel model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            return Ok(await mediator.Send(new TimeEntryListQuery(projectId, model.Offset, model.Limit), cancellationToken));
        }

        [HttpGet("projects/{projectId}/timeEntries/{timeEntryId}", Name = "GetProjectTimeEntryRoute")]
        [ProducesResponseType(typeof(TimeEntryViewModel), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid timeEntryId,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            var timeEntry = await mediator.Send(new TimeEntryQuery(projectId, timeEntryId), cancellationToken);
            
            if (timeEntry == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound, "Time entry not found");

            return Ok(timeEntry);
        }
    }
}