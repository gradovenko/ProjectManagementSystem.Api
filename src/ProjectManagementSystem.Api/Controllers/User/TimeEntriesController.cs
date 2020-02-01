using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.TimeEntries;
using ProjectManagementSystem.Domain.User.TimeEntries;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.TimeEntries;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class TimeEntriesController : ControllerBase
    {
        /// <summary>
        /// Create time entry
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="201">Time entry created</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project/issue/user/time entry activity not found</response>
        /// <response code="409">Time entry already exists with other parameters</response>
        [HttpPost("timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> CreateTimeEntry(
            CancellationToken cancellationToken,
            [FromBody] CreateTimeEntryBinding binding,
            [FromServices] TimeEntryCreationService timeEntryCreationService)
        {
            try
            {
                await timeEntryCreationService.CreateTimeEntry(binding.ProjectId, binding.IssueId, binding.Id,
                    binding.Hours, binding.Description, binding.DueDate, User.GetId(), binding.ActivityId,
                    cancellationToken);
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

            return CreatedAtRoute("GetTimeEntryRoute", new {timeEntryId = binding.Id}, null);
        }

        /// <summary>
        /// Find time entries
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Time entry list page</response>
        [HttpGet("timeEntries", Name = "GetTimeEntriesRoute")]
        [ProducesResponseType(typeof(Page<TimeEntryListItemView>), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromQuery] FindTimeEntriesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new TimeEntryListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get the time entry
        /// </summary>
        /// <param name="id">Time entry identifier</param>
        /// <response code="200">Time entry</response>
        /// <response code="404">Time entry not found</response>
        [HttpGet("timeEntries/{id}", Name = "GetTimeEntryRoute")]
        [ProducesResponseType(typeof(TimeEntryView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetTimeEntry(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var timeEntry = await mediator.Send(new TimeEntryQuery(id), cancellationToken);

            if (timeEntry == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryNotFound, "Time entry not found");

            return Ok(timeEntry);
        }
    }
}