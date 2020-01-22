using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Accounts;
using ProjectManagementSystem.Domain.User.Accounts;
using ProjectManagementSystem.Queries.User.Accounts;

namespace ProjectManagementSystem.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class AccountController : ControllerBase
    {
        /// <summary>
        /// Get account information
        /// </summary>
        /// <response code="200">200</response>
        [HttpGet("settings/account", Name = "GetAccountRoute")]
        [ProducesResponseType(typeof(UserView), 200)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator)
        {
            var user = await mediator.Send(new UserQuery(User.GetId()), cancellationToken);

            return Ok(user);
        }

        /// <summary>
        /// Update name
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="409">Name already exists</response>
        /// <response code="422">Invalid password</response>
        [HttpPut("settings/account/name")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> UpdateName(
            CancellationToken cancellationToken,
            [FromServices] UserUpdateService userUpdateService,
            [FromBody] UpdateNameBinding binding)
        {
            try
            {
                await userUpdateService.UpdateName(User.GetId(), binding.Name, binding.Password, cancellationToken);
            }
            catch (NameAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.NameAlreadyExists, "Name already exists");
            }
            catch (InvalidPasswordException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.InvalidPassword, "Invalid password");
            }

            return NoContent();
        }

        /// <summary>
        /// Update email
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="409">Email already exists</response>
        /// <response code="422">Invalid password</response>
        [HttpPut("settings/account/email")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> UpdateEmail(
            CancellationToken cancellationToken,
            [FromServices] UserUpdateService userUpdateService,
            [FromBody] UpdateEmailBinding binding)
        {
            try
            {
                await userUpdateService.UpdateEmail(User.GetId(), binding.Email, binding.Password, cancellationToken);
            }
            catch (EmailAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.EmailAlreadyExists, "Email already exists");
            }
            catch (InvalidPasswordException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.InvalidPassword, "Invalid password");
            }

            return NoContent();
        }
    }
}