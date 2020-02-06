using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Authentication;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public sealed class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Authenticate using login and password, or refresh token
        /// </summary>
        /// <param name="binding">Auth</param>
        /// <response code="200">Successfully</response>
        /// <response code="200">Bad request</response>
        [HttpPost("oauth2/token")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(typeof(ErrorView), 400)]
        public async Task<IActionResult> Authentication(CancellationToken cancellationToken,
            [FromForm] AuthenticationBinding binding,
            [FromServices] UserAuthenticationService authenticationService)
        {
            const string passwordGrantType = "password";
            const string refreshTokenGrantType = "refresh_token";

            if (string.IsNullOrEmpty(binding.GrantType))
                return BadRequest(new ErrorView(ErrorCode.InvalidRequest, "Field 'grant_type' is required"));

            switch (binding.GrantType)
            {
                case passwordGrantType:
                    try
                    {
                        if (string.IsNullOrEmpty(binding.Username))
                            return BadRequest(new ErrorView(ErrorCode.InvalidRequest, $"Field 'username' is required for '{passwordGrantType}' grant type"));

                        if (string.IsNullOrEmpty(binding.Password))
                            BadRequest(new ErrorView(ErrorCode.InvalidRequest, $"Field 'password' is required for '{passwordGrantType}' grant type"));

                        var token = await authenticationService.AuthenticationByPassword(binding.Username, binding.Password, cancellationToken);

                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        return BadRequest(new ErrorView(ErrorCode.UnauthorizedClient, "Invalid username or password"));
                    }
                case refreshTokenGrantType:
                    try
                    {
                        if (string.IsNullOrEmpty(binding.RefreshToken))
                            return BadRequest(new ErrorView(ErrorCode.InvalidRequest, "Field 'refresh_token' is required for '{refreshTokenGrantType}' grant type"));

                        var token = await authenticationService.AuthenticationByRefreshToken(binding.RefreshToken, cancellationToken);

                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        return BadRequest(new ErrorView(ErrorCode.InvalidGrant, "Invalid refresh token"));
                    }
                default:
                    return BadRequest(new ErrorView(ErrorCode.UnsupportedGrantType, $"The authorization grant type '{binding.GrantType}' is not supported. Supported authorization grant types: '{passwordGrantType}', '{refreshTokenGrantType}'"));
            }
        }
    }
}