using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;
using ProjectManagementSystem.Queries.Admin.Projects;
using ProjectManagementSystem.Queries.User.ProjectIssues;
using ProjectManagementSystem.Queries.User.Projects;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.User.Issues;
using ProjectManagementSystem.WebApi.Models.User.Projects;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class ProjectIssuesController : ControllerBase
    {
        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="id">Project identifier</param>
        /// <param name="model">Input issue bind model</param>
        [HttpPost("projects/{id}/issues")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateIssue(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromBody] CreateIssueBindModel model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IIssueRepository issueRepository)
        {
            var project = await projectRepository.Get(id, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            var tracker = await trackerRepository.Get(model.TrackerId, cancellationToken);

            if (tracker == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");

            var issue = await issueRepository.Get(model.Id, cancellationToken);

            if (issue != null)
                if (issue.Title != model.Title)
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueAlreadyExists, "Issue already exists with other parameters");

            issue = new Issue(model.Id, model.Title, model.Description, model.TrackerId);

            project.AddIssue(issue);

            await projectRepository.Save(project);

            return CreatedAtRoute("GetProjectIssue", new { id = issue.Id }, null);
        }

        /// <summary>
        /// Find issues
        /// </summary>
        /// <param name="id">Project identifier</param>
        /// <param name="model">Input query bind model</param>
        /// <returns></returns>
        [HttpGet("projects/{id}/issues", Name = "GetProjectIssuesRoute")]
        [ProducesResponseType(typeof(ProjectsView), 200)]
        public async Task<IActionResult> FindIssues(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromQuery] QueryProjectBindModel model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] IMediator mediator)
        {
            var project = await projectRepository.Get(id, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");

            return Ok(await mediator.Send(new IssueListQuery(model.Limit, model.Offset), cancellationToken));
        }


        /// <summary>
        /// Get a issue
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="issueId"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet("projects/{projectId}/issues/{issueId}", Name = "GetProjectIssueRoute")]
        [ProducesResponseType(typeof(FullProjectView), 200)]
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

            var issue = await mediator.Send(new IssueQuery(issueId), cancellationToken);

            if (issue == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueNotFound, "Issue not found");

            return Ok(project);
        }
    }
}