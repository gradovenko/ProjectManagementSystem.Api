using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.TimeEntryActivities;
using ProjectManagementSystem.Domain.Admin.TimeEntryActivities;
using ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ProducesResponseType(401)]
    public class TimeEntryActivitiesController : ControllerBase
    {
        /// <summary>
        /// Create time entry activity
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="400">Validation failed</response>
        [HttpPost("admin/timeEntryActivities")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateTimeEntryActivityBinding binding,
            [FromServices] ITimeEntryActivityRepository timeEntryActivityRepository)
        {
            var timeEntryActivity = await timeEntryActivityRepository.Get(binding.Id, cancellationToken);

            if (timeEntryActivity != null)
                if (!timeEntryActivity.Name.Equals(binding.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.TimeEntryActivityAlreadyExists,
                        "Issue status already exists with other parameters");

            timeEntryActivity = new TimeEntryActivity(binding.Id, binding.Name, binding.IsActive);

            await timeEntryActivityRepository.Save(timeEntryActivity);

            return CreatedAtRoute("GetTimeEntryActivityAdminRoute", new {id = timeEntryActivity.Id}, null);
        }

        /// <summary>
        /// Find time entry activities
        /// </summary>
        /// <param name="binding">Input model</param>
        [HttpGet("admin/timeEntryActivities")]
        [ProducesResponseType(typeof(TimeEntryActivityListItemView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindTimeEntryActivitiesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new TimeEntryActivityListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get the time entry activity
        /// </summary>
        /// <param name="id">Time entry activity identifier</param>
        [HttpGet("admin/timeEntryActivities/{id}", Name = "GetTimeEntryActivityAdminRoute")]
        [ProducesResponseType(typeof(TimeEntryActivityView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var timeEntryActivity = await mediator.Send(new TimeEntryActivityQuery(id), cancellationToken);

            if (timeEntryActivity == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TimeEntryActivityNotFound,
                    "Time entry activity not found");

            return Ok(timeEntryActivity);
        }
    }
}