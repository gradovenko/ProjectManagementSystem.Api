using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.Roles;
using ProjectManagementSystem.Domain.Admin.Roles;
using ProjectManagementSystem.Queries.Admin.Roles;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class RolesController : ControllerBase
    {
        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="binding">Input model</param>
        [HttpPost("admin/roles")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateRoleBinding binding,
            [FromServices] IRoleRepository roleRepository,
            [FromServices] IPermissionRepository permissionRepository)
        {
            var role = await roleRepository.Get(binding.Id, cancellationToken);

            if (role != null)
                if (!role.Name.Equals(binding.Name))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.RoleAlreadyExists,
                        "Role already exists with other parameters");

            role = new Role(binding.Id, binding.Name);
            
            foreach (var permissionId in binding.Permissions)
            {
                var permission = await permissionRepository.Get(permissionId, cancellationToken);
                
                if (permission == null)
                    throw new ApiException(HttpStatusCode.NotFound, ErrorCode.PermissionNotFound, "Permission not found");
                
                var rolePermission = new RolePermission(binding.Id, permission.Id);
                
                role.AddRolePermission(rolePermission);
            }

            await roleRepository.Save(role);

            return CreatedAtRoute("GetRoleAdminRoute", new {id = role.Id}, null);
        }

        /// <summary>
        /// Find roles
        /// </summary>
        /// <param name="binding">Input model</param>
        [HttpGet("admin/roles", Name = "FindRolesAdminRoute")]
        [ProducesResponseType(typeof(RoleListItemView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindRolesBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new RoleListQuery(binding.Offset, binding.Limit), cancellationToken));
        }
        
        /// <summary>
        /// Get the role
        /// </summary>
        /// <param name="id">Role identifier</param>
        [HttpGet("admin/roles/{id}", Name = "GetRoleAdminRoute")]
        [ProducesResponseType(typeof(RoleView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var role = await mediator.Send(new RoleQuery(id), cancellationToken);

            if (role == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.RoleNotFound, "Role not found");

            return Ok(role);
        }
    }
}