using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.CreateRoles;
using ProjectManagementSystem.Queries.Admin.Roles;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.Roles;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpPost("admin/roles")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateRoleBindModel model,
            [FromServices] IRoleRepository roleRepository,
            [FromServices] IPermissionRepository permissionRepository)
        {
            var role = await roleRepository.Get(model.Id, cancellationToken);

            if (role != null)
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.RoleAlreadyExists,
                    "Role already exists with other parameters");

            role = new Role(model.Id, model.Name);
            
            foreach (var permissionModel in model.Permissions)
            {
                var permission = await permissionRepository.Get(permissionModel.Id, cancellationToken);
                
                if (permission == null)
                    throw new ApiException(HttpStatusCode.NotFound, ErrorCode.TrackerNotFound, "Tracker not found");
                
                var rolePermission = new RolePermission(model.Id, permission.Id);
                
                role.AddRolePermission(rolePermission);
            }

            await roleRepository.Save(role);

            return CreatedAtRoute("GetRoleAdminRoute", new {id = role.Id}, null);
        }

        [HttpGet("admin/roles")]
        [ProducesResponseType(typeof(RoleListViewModel), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryRoleBindModel model,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new RoleListQuery(model.Offset, model.Limit), cancellationToken));
        }

        [HttpGet("admin/roles/{id}", Name = "GetRoleAdminRoute")]
        [ProducesResponseType(typeof(RoleViewModel), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var role = await mediator.Send(new RoleQuery(id), cancellationToken);

            if (role == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.IssueStatusNotFound,
                    "Issue status not found");

            return Ok(role);
        }
    }
}