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
    public sealed class IssuesController : ControllerBase
    {
        [HttpPost("issues")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateIssue(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
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
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");
            }
            catch (TrackerNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");
            }
            catch (IssueStatusNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueStatusNotFound,
                    "Issue status not found");
            }
            catch (IssuePriorityNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Issue priority not found");
            }
            catch (AssigneeNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.AssigneeNotFound, "Assignee not found");
            }
            catch (IssueAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueAlreadyExists,
                    "Issue already exists with other parameters");
            }

            return CreatedAtRoute("GetIssueRoute", new {issueId = binding.Id}, null);
        }

        [HttpGet("issues", Name = "GetIssuesRoute")]
        [ProducesResponseType(typeof(Page<IssueListView>), 200)]
        public async Task<IActionResult> FindIssues(
            CancellationToken cancellationToken,
            [FromQuery] FindIssuesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new IssueListQuery(binding.Offset, binding.Limit), cancellationToken));
        }

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