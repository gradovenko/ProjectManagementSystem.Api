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
using ProjectManagementSystem.WebApi.Extensions;
using ProjectManagementSystem.WebApi.Models.User.ProjectIssues;

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
        /// <param name="model">Input create bind model</param>
        [HttpPost("projects/{id}/issues")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateIssue(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromBody] CreateIssueBindModel model,
            [FromServices] IssueCreationService issueCreationService)
        {
            try
            {
                await issueCreationService.CreateIssue(id, model.Id, model.Title, model.Description, model.StartDate,
                    model.EndDate, model.TrackerId, model.StatusId, model.PriorityId, User.GetId(), model.PerformerId,
                    cancellationToken);
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
            catch (PerformerNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.PerformerNotFound, "Performer not found");
            }
            catch (IssueAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.IssueAlreadyExists,
                    "Issue already exists with other parameters");
            }

            return CreatedAtRoute("GetProjectIssue", new {id = model.Id}, null);
        }

        /// <summary>
        /// Find issues
        /// </summary>
        /// <param name="id">Project identifier</param>
        /// <param name="model">Input query bind model</param>
        [HttpGet("projects/{id}/issues", Name = "GetProjectIssuesRoute")]
        [ProducesResponseType(typeof(ProjectsView), 200)]
        public async Task<IActionResult> FindIssues(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromQuery] QueryIssueBindModel model,
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
        /// <param name="issueId">Issue identifier</param>
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