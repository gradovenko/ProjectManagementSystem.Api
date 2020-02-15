using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.Admin.Members;
using ProjectManagementSystem.Domain.Admin.Members;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class MembersController : ControllerBase
    {
        /// <summary>
        /// Create member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="binding">Input model</param>
        [HttpPost("admin/users/{id}/members")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromBody] CreateMemberBinding binding,
            [FromServices] IUserRepository userRepository)
        {
            var user = await userRepository.Get(id, cancellationToken);

            user.AddMember(new Member(binding.Id, id, binding.ProjectId, binding.RoleId));

            await userRepository.Save(user);

            return CreatedAtRoute("GetMemberAdminRoute", new {id = binding.Id}, null);
        }
    }
}