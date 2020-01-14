using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.Trackers;
using ProjectManagementSystem.Domain.Admin.Trackers;
using ProjectManagementSystem.Queries.Admin.Trackers;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    public class TrackersController : ControllerBase
    {
        /// <summary>
        /// Create tracker
        /// </summary>
        /// <param name="model">Input model</param>
        [HttpPost("admin/trackers")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateTrackerBinding model,
            [FromServices] ITrackerRepository trackerRepository)
        {
            var tracker = await trackerRepository.Get(model.Id, cancellationToken);

            if (tracker != null)
                if (!tracker.Name.Equals(model.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.TrackerAlreadyExists, "Tracker already exists with other parameters");

            tracker = new Tracker(model.Id, model.Name);

            await trackerRepository.Save(tracker);
            
            return CreatedAtRoute("GetTrackerAdminRoute", new { id = tracker.Id }, null);
        }

        /// <summary>
        /// Find trackers
        /// </summary>
        /// <param name="binding"></param>
        [HttpGet("admin/trackers", Name = "GetTrackersAdminRoute")]
        [ProducesResponseType(typeof(ShortTrackerView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindTrackersBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new TrackersQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get tracker
        /// </summary>
        /// <param name="id">Tracker identifier</param>
        [HttpGet("admin/trackers/{id}", Name = "GetTrackerAdminRoute")]
        [ProducesResponseType(typeof(FullTrackerView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var tracker = await mediator.Send(new TrackerQuery(id), cancellationToken);

            if (tracker == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");

            return Ok(tracker);
        }
    }
}