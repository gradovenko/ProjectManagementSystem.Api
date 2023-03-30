using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Queries.User.Profiles;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class ProfileController : ControllerBase
{
    /// <summary>
    /// Get the profile
    /// </summary>
    /// <response code="200">Profile information</response>
    [HttpGet("/profile", Name = "Get")]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromServices] IMediator mediator)
    {
        UserViewModel user = await mediator.Send(new UserQuery(User.GetId()), cancellationToken);

        return Ok(user);
    }

    // /// <summary>
    // /// Update the profile
    // /// </summary>
    // /// <param name="binding">Input model</param>
    // /// <response code="200">Set name</response>
    // /// <response code="409">Name already exists</response>
    // /// <response code="422">Invalid password</response>
    // [HttpPut("/profile")]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(typeof(ProblemDetails), 409)]
    // [ProducesResponseType(typeof(ProblemDetails), 422)]
    // public async Task<IActionResult> UpdateName(
    //     CancellationToken cancellationToken,
    //     [FromServices] UserUpdateService userUpdateService,
    //     [FromBody] UpdateNameBinding binding)
    // {
    //     try
    //     {
    //         await userUpdateService.UpdateName(User.GetId(), binding.Name, binding.Password, cancellationToken);
    //     }
    //     catch (NameAlreadyExistsException)
    //     {
    //         throw new ApiException(HttpStatusCode.Conflict, ErrorCode.NameAlreadyExists, "Name already exists");
    //     }
    //     catch (InvalidPasswordException)
    //     {
    //         throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.InvalidPassword, "Invalid password");
    //     }
    //
    //     return NoContent();
    // }
}