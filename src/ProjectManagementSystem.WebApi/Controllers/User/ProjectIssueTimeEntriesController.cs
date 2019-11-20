using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries.User.TimeEntries;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Models.User.TimeEntries;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class ProjectIssueTimeEntriesController : ControllerBase
    {
        [HttpPost("projects/{projectId}/issues/{issueId}/timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid issueId,
            [FromBody] CreateTimeEntryBindModel model,
            [FromServices] TimeEntryCreationService timeEntryCreationService)
        {
            try
            {
                await timeEntryCreationService.CreateTimeEntry(projectId, issueId, model.Id, model.Hours,
                    model.Description, model.DueDate, model.UserId, model.ActivityId, cancellationToken);
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

            return CreatedAtRoute("GetProjectIssueTimeEntryRoute", new {projectId, issueId, timeEntryId = model.Id}, null);
        }

        [HttpGet("projects/{projectId}/issues/{issueId}/timeEntries", Name = "GetProjectIssueTimeEntriesRoute")]
        [ProducesResponseType(typeof(TimeEntryListViewModel), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid issueId,
            [FromQuery] QueryPageBindModel model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IIssueRepository issueRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");
            
            var issue = await issueRepository.Get(issueId, cancellationToken);

            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");

            return Ok(await mediator.Send(new TimeEntryListQuery(projectId, issueId, model.Offset, model.Limit), cancellationToken));
        }

        [HttpGet("projects/{projectId}/issues/{issueId}/timeEntries/{timeEntryId}", Name = "GetProjectIssueTimeEntryRoute")]
        [ProducesResponseType(typeof(TimeEntryViewModel), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid issueId,
            [FromRoute] Guid timeEntryId,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IIssueRepository issueRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");
            
            var issue = await issueRepository.Get(issueId, cancellationToken);

            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");
            
            var timeEntry = await mediator.Send(new TimeEntryQuery(projectId, issueId, timeEntryId), cancellationToken);
            
            if (timeEntry == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound, "Time entry not found");

            return Ok(timeEntry);
        }
    }
}