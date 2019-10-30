using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Queries.User.Projects;
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
    }
}