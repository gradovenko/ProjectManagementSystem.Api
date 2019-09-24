using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.IssuePriorities;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.IssuePriorities;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class IssuePrioritiesController : ControllerBase
    {
        /// <summary>
        /// Create issue priority
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="issuePriorityRepository"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
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
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="queryProcessor"></param>
        /// <returns></returns>
        [HttpGet("admin/issuePriorities")]
        [ProducesResponseType(typeof(IssuePriorityView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryIssuePriorityBinding binding,
            [FromServices] IQueryProcessor queryProcessor)
        {
            return Ok(await queryProcessor.ProcessAsync(new IssuePrioritiesQuery(binding.Offset, binding.Limit), cancellationToken));
        }
        
        /// <summary>
        /// Get issue priority
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id">User ID</param>
        /// <param name="queryProcessor"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet("admin/issuePriorities/{id}", Name = "GetIssuePriorityAdminRoute")]
        [ProducesResponseType(typeof(IssuePriorityView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IQueryProcessor queryProcessor)
        {
            var issuePriority = await queryProcessor.ProcessAsync(new IssuePriorityQuery(id), cancellationToken);

            if (issuePriority == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssuePriorityNotFound, "Issue priority not found");

            return Ok(issuePriority);
        }
    }
}