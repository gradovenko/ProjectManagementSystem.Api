using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.IssueStatuses;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;
using ProjectManagementSystem.Queries.Admin.IssueStatuses;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class IssueStatusesController : ControllerBase
    {
        /// <summary>
        /// Create issue status
        /// </summary>
        /// <param name="binding"></param>
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
        /// <param name="binding"></param>
        [HttpGet("admin/issueStatuses")]
        [ProducesResponseType(typeof(ShortIssueStatusView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindIssueStatusesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new IssueStatusesQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get issue status
        /// </summary>
        /// <param name="id">User identifier</param>
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