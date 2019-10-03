using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.CreateProjectTrackers;
using ProjectManagementSystem.WebApi.Models.Admin.ProjectTrackers;
using ProjectManagementSystem.WebApi.Exceptions;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ProjectTrackersController : ControllerBase
    {
        /// <summary>
        /// Add trackers for the project
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id">Project id</param>
        /// <param name="model">Input model</param>
        /// <param name="projectRepository"></param>
        /// <param name="trackerRepository"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("admin/projects/{id}/trackers")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> AddProjectTrackers(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromBody] AddProjectTrackersBindModel model,
            [FromServices] IProjectRepository projectRepository,
            [FromServices] ITrackerRepository trackerRepository)
        {
            var project = await projectRepository.Get(id, cancellationToken);

            if (project == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.ProjectNotFound, "Project not found");
            
            foreach (var trackerModel in model.Trackers)
            {
                var tracker = await trackerRepository.Get(trackerModel.Id, cancellationToken);
                
                if (tracker == null)
                    throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");
                
                var projectTracker = new ProjectTracker(id, tracker.Id);
                
                project.AddProjectTracker(projectTracker);
            }
            
            await projectRepository.Save(project);
            
            return CreatedAtRoute("GetProjectTrackersAdminRoute", new {id = project.Id}, null);
        }
    }
}