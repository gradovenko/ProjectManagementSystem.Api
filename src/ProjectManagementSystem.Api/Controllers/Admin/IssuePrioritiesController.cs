using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.IssuePriorities;
using ProjectManagementSystem.Domain.Admin.IssuePriorities;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class IssuePrioritiesController : ControllerBase
    {
        /// <summary>
        /// Create issue priority
        /// </summary>
        /// <param name="binding"></param>
        [HttpPost("admin/issuePriorities")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateIssuePriorityBinding binding, 
            [FromServices] IIssuePriorityRepository issuePriorityRepository)
        {
            var issuePriority = await issuePriorityRepository.Get(binding.Id, cancellationToken);

            if (issuePriority != null)
                if (!issuePriority.Name.Equals(binding.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssuePriorityAlreadyExists, "Issue priority already exists with other parameters");

            issuePriority = new IssuePriority(binding.Id, binding.Name, binding.IsActive);

            await issuePriorityRepository.Save(issuePriority);

            return CreatedAtRoute("GetIssuePriorityAdminRoute", new { id = issuePriority.Id }, null);
        }
        
        /// <summary>
        /// Find issue priorities
        /// </summary>
        /// <param name="binding"></param>
        [HttpGet("admin/issuePriorities")]
        [ProducesResponseType(typeof(IssuePriorityView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindIssuePrioritiesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new IssuePriorityListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get issue priority
        /// </summary>
        /// <param name="id">Issue priority identifier</param>
        [HttpGet("admin/issuePriorities/{id}", Name = "GetIssuePriorityAdminRoute")]
        [ProducesResponseType(typeof(IssuePriorityView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var issuePriority = await mediator.Send(new IssuePriorityQuery(id), cancellationToken);

            if (issuePriority == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssuePriorityNotFound, "Issue priority not found");

            return Ok(issuePriority);
        }
    }
}