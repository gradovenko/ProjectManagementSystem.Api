using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Issues;
using ProjectManagementSystem.Domain.User.Issues;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.Issues;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class IssuesController : ControllerBase
    {
        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="201">Created issue</response>
        /// <response code="400">Validation failed</response>
        /// <response code="409">Issue already exists with other parameters</response>
        /// <response code="422">Project/tracker/issue status/issue priority/assignee not found</response>
        [HttpPost("issues")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> CreateIssue(
            CancellationToken cancellationToken,
            [FromBody] CreateIssueBinding binding,
            [FromServices] IssueCreationService issueCreationService)
        {
            try
            {
                await issueCreationService.CreateIssue(binding.Id, binding.Title, binding.Description, binding.StartDate,
                    binding.DueDate, binding.ProjectId, binding.TrackerId, binding.StatusId, binding.PriorityId, User.GetId(),
                    binding.AssigneeId, cancellationToken);
            }
            catch (ProjectNotFoundException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.ProjectNotFound, "Project not found");
            }
            catch (TrackerNotFoundException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.TrackerNotFound, "Tracker not found");
            }
            catch (IssueStatusNotFoundException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.IssueStatusNotFound, "Issue status not found");
            }
            catch (IssuePriorityNotFoundException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.IssuePriorityNotFound, "Issue priority not found");
            }
            catch (AssigneeNotFoundException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.AssigneeNotFound, "Assignee not found");
            }
            catch (IssueAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueAlreadyExists, "Issue already exists with other parameters");
            }

            return CreatedAtRoute("GetIssueRoute", new {issueId = binding.Id}, null);
        }

        /// <summary>
        /// Find issues
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Issue list page</response>
        [HttpGet("issues", Name = "GetIssuesRoute")]
        [ProducesResponseType(typeof(Page<IssueListItemView>), 200)]
        public async Task<IActionResult> FindIssues(
            CancellationToken cancellationToken,
            [FromQuery] FindIssuesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new IssueListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

        /// <summary>
        /// Get the issue
        /// </summary>
        /// <param name="id">Issue identifier</param>
        /// <response code="200">Issue</response>
        /// <response code="404">Issue not found</response>
        [HttpGet("issues/{id}", Name = "GetIssueRoute")]
        [ProducesResponseType(typeof(IssueView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetIssue(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var issue = await mediator.Send(new IssueQuery(id), cancellationToken);

            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");

            return Ok(issue);
        }
    }
}