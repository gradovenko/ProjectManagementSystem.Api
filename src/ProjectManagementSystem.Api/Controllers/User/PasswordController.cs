using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Password;
using ProjectManagementSystem.Domain.Users.Commands;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class PasswordController : ControllerBase
{
    private readonly IMediator _mediator;

    public PasswordController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    /// <summary>
    /// Change user password via old password
    /// </summary>
    /// <param name="model">Password change model</param>
    /// <response code="204">Successfully</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Email not found</response>
    /// <response code="422">User not found or wrong old password</response>
    [Authorize]
    [HttpPost("/password/change")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 401)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> ChangePasswordViaOldPassword(
        CancellationToken cancellationToken,
        [FromBody] ChangePasswordViaOldPasswordBindingModel model)
    {
        ChangeUserPasswordViaOldPasswordCommandResultState commandResultState = await _mediator.Send(new ChangeUserPasswordViaOldPasswordCommand
        {
            UserId = User.GetId(),
            OldPassword = model.OldPassword,
            NewPassword = model.NewPassword
        }, cancellationToken);

        return commandResultState switch
        {
            ChangeUserPasswordViaOldPasswordCommandResultState.UserNotFound => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
                HttpContext.Request.Path),
            ChangeUserPasswordViaOldPasswordCommandResultState.UserPasswordWrong => this.StatusCode(HttpStatusCode.UnprocessableEntity, ErrorCode.OldUserPasswordWrong.Title, ErrorCode.OldUserPasswordWrong.Detail,
                HttpContext.Request.Path),
            ChangeUserPasswordViaOldPasswordCommandResultState.UserPasswordChanged => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}