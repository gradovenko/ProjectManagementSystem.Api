using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.IssueTimeEntries;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.IssueTimeEntries;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class IssueTimeEntriesController : ControllerBase
    {
        /// <summary>
        /// Create issue time entry
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="issueId">Issue identifier</param>
        /// <param name="binding">Input model</param>
        /// <response code="201">Successfully</response>
        /// <response code="400">Validation failed</response>
        /// <response code="409">Issue already exists with other parameters</response>
        /// <response code="422">Project/tracker/issue status/issue priority/assignee not found</response>
        [HttpPost("issues/{issueId}/timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid projectId,
            [FromRoute] Guid issueId,
            [FromBody] CreateTimeEntryBinding binding,
            [FromServices] TimeEntryCreationService timeEntryCreationService)
        {
            try
            {
                await timeEntryCreationService.CreateTimeEntry(projectId, issueId, binding.Id, binding.Hours,
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

            return CreatedAtRoute("GetIssueTimeEntryRoute", new {issueId, timeEntryId = binding.Id}, null);
        }

        /// <summary>
        /// Find issue time entries
        /// </summary>
        /// <param name="id">Issue identifier</param>
        /// <param name="binding">Input model</param>
        /// <response code="200">Time entry list page</response>
        [HttpGet("issues/{id}/timeEntries", Name = "GetIssueTimeEntriesRoute")]
        [ProducesResponseType(typeof(Page<TimeEntryListItemView>), 200)]
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

        /// <summary>
        /// Get the project time entry
        /// </summary>
        /// <param name="issueId">Issue identifier</param>
        /// <param name="timeEntryId">Time entry identifier</param>
        /// <response code="201">Time entry</response>
        /// <response code="404">Time entry not found</response>
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