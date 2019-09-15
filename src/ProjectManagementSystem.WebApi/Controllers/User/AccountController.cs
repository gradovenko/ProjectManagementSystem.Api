using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.WebApi.Extensions;
using ProjectManagementSystem.Domain.User.Accounts;
using ProjectManagementSystem.Queries.User.Accounts;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.User.Accounts;

namespace ProjectManagementSystem.WebApi.Controllers.User
{
    [Authorize]
    [ApiController]
    public sealed class AccountController : ControllerBase
    {
        /// <summary>
        /// Get my name and email
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="queryProcessor"></param>
        /// <returns></returns>
        [HttpGet("settings/account", Name = "GetAccountRoute")]
        [ProducesResponseType(typeof(UserView), 200)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromServices] IQueryProcessor queryProcessor)
        {
            var user = await queryProcessor.ProcessAsync(new UserQuery(User.GetId()), cancellationToken);

            return Ok(user);
        }

        /// <summary>
        /// Update my name
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="userUpdateService"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        [HttpPut("settings/account/name")]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        [ProducesResponseType(204)]
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
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.UsernameAlreadyExists, "Name already exists");
            }
            catch (InvalidPasswordException)
            {
                throw new ApiException(HttpStatusCode.UnprocessableEntity, ErrorCode.InvalidPassword, "Invalid password");
            }

            return NoContent();
        }

        /// <summary>
        /// Update my email
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="userUpdateService"></param>
        /// <param name="binding">Input model</param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
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