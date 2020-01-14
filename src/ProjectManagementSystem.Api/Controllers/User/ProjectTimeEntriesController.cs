using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.ProjectTimeEntries;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.ProjectTimeEntries;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class ProjectTimeEntriesController : ControllerBase
    {
        [HttpPost("projects/{projectId}/timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromBody] CreateTimeEntryBinding binding,
            [FromServices] TimeEntryCreationService timeEntryCreationService)
        {
            try
            {
                await timeEntryCreationService.CreateTimeEntry(projectId, binding.IssueId, binding.Id, binding.Hours,
                    binding.Description, binding.DueDate, User.GetId(), binding.ActivityId, cancellationToken);
            }
            catch (ProjectNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");
            }
            catch (IssueNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");
            }
            catch (UserNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.UserNotFound, "User not found");
            }
            catch (TimeEntryActivityNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryActivityNotFound,
                    "Time entry activity not found");
            }
            catch (TimeEntryAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.TimeEntryAlreadyExists,
                    "Time entry already exists with other parameters");
            }
            
            return CreatedAtRoute("GetProjectTimeEntryRoute", new {projectId, timeEntryId = binding.Id}, null);
        }

        [HttpGet("projects/{projectId}/timeEntries", Name = "GetProjectTimeEntriesRoute")]
        [ProducesResponseType(typeof(Page<TimeEntryListView>), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromQuery] FindTimeEntriesBinding binding,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            return Ok(await mediator.Send(new TimeEntryListQuery(projectId, binding.Offset, binding.Limit), cancellationToken));
        }

        [HttpGet("projects/{projectId}/timeEntries/{timeEntryId}", Name = "GetProjectTimeEntryRoute")]
        [ProducesResponseType(typeof(TimeEntryView), 200)]
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