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
        /// <param name="binding">Input model</param>
        [HttpPost("admin/users/{id}/membership")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromBody] CreateMembersBinding binding,
            //[FromServices] IUser roleRepository,
            [FromServices] IUserRepository userRepository)
        {
            var user = await userRepository.Get(id, cancellationToken);

            foreach (var member in binding.Members)
            {
                user.AddMember(new Member(member.Id, id, member.ProjectId, member.RoleId));
            }

            await userRepository.Save(user);

            return CreatedAtRoute("GetMemberAdminRoute", new {id}, null);
        }
    }
}