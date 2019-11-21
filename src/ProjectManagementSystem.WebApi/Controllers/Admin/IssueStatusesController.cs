using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;
using ProjectManagementSystem.Queries.Admin.IssueStatuses;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.IssueStatuses;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class IssueStatusesController : ControllerBase
    {
        /// <summary>
        /// Create issue status
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="issueStatusRepository"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("admin/issueStatuses")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateIssueStatusBinding binding, 
            [FromServices] IIssueStatusRepository issueStatusRepository)
        {
            var issueStatus = await issueStatusRepository.Get(binding.Id, cancellationToken);

            if (issueStatus != null)
                if (!issueStatus.Name.Equals(binding.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueStatusAlreadyExists, "Issue status already exists with other parameters");

            issueStatus = new IssueStatus(binding.Id, binding.Name, binding.IsActive);

            await issueStatusRepository.Save(issueStatus);

            return CreatedAtRoute("GetIssueStatusAdminRoute", new { id = issueStatus.Id }, null);
        }
        
        /// <summary>
        /// Find issue statuses
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet("admin/issueStatuses")]
        [ProducesResponseType(typeof(ShortIssueStatusView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryIssueStatusBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new IssueStatusesQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get issue status
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id">User ID</param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet("admin/issueStatuses/{id}", Name = "GetIssueStatusAdminRoute")]
        [ProducesResponseType(typeof(ShortIssueStatusView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var issueStatus = await mediator.Send(new IssueStatusQuery(id), cancellationToken);

            if (issueStatus == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueStatusNotFound, "Issue status not found");

            return Ok(issueStatus);
        }
    }
}