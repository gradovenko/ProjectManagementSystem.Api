using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;
using ProjectManagementSystem.Queries.User.Projects;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.User.Issues;
using ProjectManagementSystem.WebApi.Models.User.Projects;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Find projects
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="model">Input query bind model</param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet("projects", Name = "GetProjectsRoute")]
        [ProducesResponseType(typeof(ProjectsView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryProjectBindModel model,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ProjectsQuery(model.Limit, model.Offset), cancellationToken));
        }
        
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
    }
}