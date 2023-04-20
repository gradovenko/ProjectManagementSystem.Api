using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Authorization;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Domain.Users.Commands;
using ProjectManagementSystem.Queries;

namespace ProjectManagementSystem.Api.Controllers;

/// <summary>
/// Users controller
/// </summary>
/// <response code="401">Unauthorized</response>
[Authorize(Roles = UserRole.AdminAndUser)]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), 401)]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    #region Admin

    /// <summary>
    /// Create the user as admin
    /// </summary>
    /// <param name="model">Input model</param>
    /// <response code="201">User created</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="409">User with same name or email already exists</response>
    /// <response code="409">User with same id but different params already exists</response>
    [HttpPost("/admin/users")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        [FromBody] Models.Admin.Users.CreateUserBindingModel model)
    {
        CreateUserAsAdminCommandResultState commandResultState = await _mediator.Send(new CreateUserAsAdminCommand
        {
            UserId = model.Id,
            Name = model.Name,
            Email = model.Email,
            Password = model.Password,
            Role = model.Role,
        }, cancellationToken);

        return commandResultState switch
        {
            CreateUserAsAdminCommandResultState.UserWithSameNameAlreadyExists => this.StatusCode(
                HttpStatusCode.Conflict, ErrorCode.UserWithSameNameAlreadyExists.Title,
                ErrorCode.UserWithSameNameAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateUserAsAdminCommandResultState.UserWithSameEmailAlreadyExists => this.StatusCode(
                HttpStatusCode.Conflict, ErrorCode.UserWithSameEmailAlreadyExists.Title,
                ErrorCode.UserWithSameNameAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateUserAsAdminCommandResultState.UserWithSameIdButDifferentParamsAlreadyExists => this.StatusCode(
                HttpStatusCode.Conflict, ErrorCode.UserWithSameIdButDifferentParamsAlreadyExists.Title,
                ErrorCode.UserWithSameIdButDifferentParamsAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateUserAsAdminCommandResultState.UserCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of users as admin
    /// </summary>
    /// <response code="200">User list</response>
    [HttpGet("/admin/users", Name = "GetAdminUserList")]
    [ProducesResponseType(typeof(Page<Queries.Admin.Users.UserListItemViewModel>), 200)]
    public async Task<IActionResult> GetAdminList(
        CancellationToken cancellationToken,
        [FromQuery] Models.Admin.Users.GetUserListBindingModel model)
    {
        return Ok(await _mediator.Send(new Queries.Admin.Users.UserListQuery(model.Offset, model.Limit), cancellationToken));
    }

    /// <summary>
    /// Update the user as admin
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <response code="204">User updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">User with this identifier not found</response>
    /// <response code="409">User with same name already exists</response>
    [HttpPut("/admin/users/{userId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid userId,
        [FromBody] Models.Admin.Users.UpdateUserBindingModel model)
    {
        UpdateUserAsAdminCommandResultState commandResultState = await _mediator.Send(new UpdateUserAsAdminCommand
        {
            UserId = userId,
            Name = model.Name
        }, cancellationToken);

        return commandResultState switch
        {
            UpdateUserAsAdminCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            UpdateUserAsAdminCommandResultState.UserWithSameNameAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.UserWithSameNameAlreadyExists.Title, ErrorCode.UserWithSameNameAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateUserAsAdminCommandResultState.UserUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    /// <summary>
    /// Delete the user as admin
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <response code="204">User deleted</response>
    /// <response code="404">User not found</response>
    [HttpDelete("/admin/users/{userId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken,
        [FromRoute] Guid userId)
    {
        DeleteUserAsAdminCommandResultState commandResultState = await _mediator.Send(new DeleteUserAsAdminCommand
        {
            UserId = userId
        }, cancellationToken);

        return commandResultState switch
        {
            DeleteUserAsAdminCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            DeleteUserAsAdminCommandResultState.UserDeleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}