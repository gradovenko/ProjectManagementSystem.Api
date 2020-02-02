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
            [FromBody] CreateMemberBinding binding,
            //[FromServices] IUser roleRepository,
            [FromServices] IUserRepository userRepository)
        {
            //
            
            var user = await userRepository.Get(binding.Id, cancellationToken);
            
            // if (role != null)
            //     if (!role.Name.Equals(binding.Name))
            //         throw new ApiException(HttpStatusCode.Conflict, ErrorCode.RoleAlreadyExists,
            //             "Role already exists with other parameters");
            //
            // role = new Role(binding.Id, binding.Name);
            
            foreach (var projectRole in binding.ProjectRoles)
            {
                user.AddMember(new Member(Guid.NewGuid(), id, projectRole.ProjectId));

                foreach (var member in user.Members)
                {
                    member.
                }
                
                //
                // var permission = await permissionRepository.Get(permissionId, cancellationToken);
                //
                // if (permission == null)
                //     throw new ApiException(HttpStatusCode.NotFound, ErrorCode.PermissionNotFound, "Permission not found");
                //
                // var rolePermission = new RolePermission(binding.Id, permission.Id);
                //
                // role.AddRolePermission(rolePermission);
            }

            await roleRepository.Save(role);

            return CreatedAtRoute("GetRoleAdminRoute", new {id = role.Id}, null);
        }
    }
}