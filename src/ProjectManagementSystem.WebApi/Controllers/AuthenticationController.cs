using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Authentication;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Authenticate using login and password, or refresh token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="authenticationService"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("auth/token")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        public async Task<IActionResult> Authentication(CancellationToken cancellationToken,
            [FromBody] SignInBinding binding,
            [FromServices] UserAuthenticationService authenticationService)
        {
            switch (binding.GrantType)
            {
                case GrantType.password:
                    try
                    {
                        var token = await authenticationService.AuthenticationByPassword(binding.Login,
                            binding.Password, cancellationToken);
                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        throw new ApiException(HttpStatusCode.Unauthorized, ErrorCode.InvalidCredentials,
                            "Login or password is incorrect");
                    }
                case GrantType.refresh_token:
                    try
                    {
                        var token = await authenticationService.AuthenticationByRefreshToken(binding.RefreshToken,
                            cancellationToken);
                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        throw new ApiException(HttpStatusCode.Unauthorized, ErrorCode.InvalidCredentials,
                            "Refresh token is incorrect");
                    }
                default:
                    throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.UnsupportedGrantType,
                        "Unsupported grant type");
            }
        }
    }
}