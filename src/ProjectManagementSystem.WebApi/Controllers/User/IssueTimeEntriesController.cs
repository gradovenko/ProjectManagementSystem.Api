using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.IssueTimeEntries;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Extensions;
using ProjectManagementSystem.WebApi.Models.User.IssueTimeEntries;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class IssueTimeEntriesController : ControllerBase
    {
        [HttpPost("issues/{issueId}/timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid issueId,
            [FromBody] CreateTimeEntryBinding model,
            [FromServices] TimeEntryCreationService timeEntryCreationService)
        {
            try
            {
                await timeEntryCreationService.CreateTimeEntry(projectId, issueId, model.Id, model.Hours,
                    model.Description, model.DueDate, User.GetId(), model.ActivityId, cancellationToken);
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

        [HttpGet("issues/{id}/timeEntries", Name = "GetIssueTimeEntriesRoute")]
        [ProducesResponseType(typeof(Page<TimeEntryListView>), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromQuery] FindTimeEntriesBinding binding,
            [FromServices] IIssueRepository issueRepository,
            [FromServices] IMediator mediator)
        {
            var issue = await issueRepository.Get(id, cancellationToken);

            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");

            return Ok(await mediator.Send(new TimeEntryListQuery(id, binding.Offset, binding.Limit), cancellationToken));
        }

        [HttpGet("issues/{issueId}/timeEntries/{timeEntryId}", Name = "GetIssueTimeEntryRoute")]
        [ProducesResponseType(typeof(TimeEntryView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid issueId,
            [FromRoute] Guid timeEntryId,
            [FromServices] IIssueRepository issueRepository,
            [FromServices] IMediator mediator)
        {
            var issue = await issueRepository.Get(issueId, cancellationToken);
        
            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");
            
            var timeEntry = await mediator.Send(new TimeEntryQuery(issueId, timeEntryId), cancellationToken);
            
            if (timeEntry == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound, "Time entry not found");
        
            return Ok(timeEntry);
        }
    }
}