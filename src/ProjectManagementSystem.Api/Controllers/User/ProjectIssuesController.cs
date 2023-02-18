using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.ProjectIssues;
using ProjectManagementSystem.Domain.User.Issues;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class ProjectIssuesController : ControllerBase
{
    /// <summary>
    /// Create project issue
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <param name="binding">Input model</param>
    /// <response code="400">Validation failed</response>
    [HttpPost("projects/{id}/issues")]
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
                binding.DueDate, id, binding.TrackerId, binding.StatusId, binding.PriorityId, User.GetId(),
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
            throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueStatusNotFound, "Issue status not found");
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
            throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueAlreadyExists, "Issue already exists with other parameters");
        }

        return CreatedAtRoute("GetProjectIssueRoute", new {projectId = id, issueId = binding.Id}, null);
    }

    /// <summary>
    /// Find project issues
    /// </summary>
    /// <param name="id">Project identifier</param>
    /// <param name="binding">Input model</param>
    [HttpGet("projects/{id}/issues", Name = "GetProjectIssuesRoute")]
    [ProducesResponseType(typeof(Page<IssueListItemView>), 200)]
    public async Task<IActionResult> FindIssues(
        CancellationToken cancellationToken,
        [FromRoute] Guid id,
        [FromQuery] FindProjectIssuesBinding binding,
        [FromServices] IProjectRepository projectRepository,
        [FromServices] IMediator mediator)
    {
        var project = await projectRepository.Get(id, cancellationToken);

        if (project == null)
            throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

        return Ok(await mediator.Send(new IssueListQuery(id, binding.Offset, binding.Limit), cancellationToken));
    }


    /// <summary>
    /// Get the project issue
    /// </summary>
    /// <param name="projectId">Project identifier</param>
    /// <param name="issueId">Issue identifier</param>
    [HttpGet("projects/{projectId}/issues/{issueId}", Name = "GetProjectIssueRoute")]
    [ProducesResponseType(typeof(IssueView), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetIssue(
        CancellationToken cancellationToken,
        [FromRoute] Guid projectId,
        [FromRoute] Guid issueId,
        [FromServices] IProjectRepository projectRepository,
        [FromServices] IMediator mediator)
    {
        var project = await projectRepository.Get(projectId, cancellationToken);

        if (project == null)
            throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

        var issue = await mediator.Send(new IssueQuery(projectId, issueId), cancellationToken);

        if (issue == null)
            throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");

        return Ok(issue);
    }
}