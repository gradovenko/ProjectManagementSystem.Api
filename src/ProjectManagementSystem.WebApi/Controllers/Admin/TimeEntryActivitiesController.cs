using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.TimeEntryActivities;
using ProjectManagementSystem.Queries.Admin.TimeEntryActivities;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class TimeEntryActivitiesController : ControllerBase
    {
        /// <summary>
        /// Create time entry activity
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("admin/timeEntryActivities")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateTimeEntryActivityBindModel model,
            [FromServices] ITimeEntryActivityRepository timeEntryActivityRepository)
        {
            var timeEntryActivity = await timeEntryActivityRepository.Get(model.Id, cancellationToken);

            if (timeEntryActivity != null)
                if (!timeEntryActivity.Name.Equals(model.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.TimeEntryActivityAlreadyExists,
                        "Issue status already exists with other parameters");

            timeEntryActivity = new TimeEntryActivity(model.Id, model.Name, model.IsActive);

            await timeEntryActivityRepository.Save(timeEntryActivity);

            return CreatedAtRoute("GetTimeEntryActivityAdminRoute", new {id = timeEntryActivity.Id}, null);
        }

        /// <summary>
        /// Find time entry activities
        /// </summary>
        /// <param name="model"></param>
        [HttpGet("admin/timeEntryActivities")]
        [ProducesResponseType(typeof(TimeEntryActivityListViewModel), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryTimeEntryActivityBindModel model,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new TimeEntryActivityListQuery(model.Limit, model.Offset), cancellationToken));
        }

        /// <summary>
        /// Get time entry activity
        /// </summary>
        /// <param name="id">Time entry activity identifier</param>
        [HttpGet("admin/timeEntryActivities/{id}", Name = "GetTimeEntryActivityAdminRoute")]
        [ProducesResponseType(typeof(TimeEntryActivityViewModel), 200)]
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