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
        [HttpPost("timeEntries")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
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
        /// Get time entries
        /// </summary>
        /// <param name="binding">Input model</param>
        [HttpGet("timeEntries", Name = "GetTimeEntriesRoute")]
        [ProducesResponseType(typeof(Page<TimeEntryListView>), 200)]
        public async Task<IActionResult> FindTimeEntries(
            CancellationToken cancellationToken,
            [FromQuery] FindTimeEntriesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new TimeEntryListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get time entry
        /// </summary>
        /// <param name="id">Time entry identifier</param>
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