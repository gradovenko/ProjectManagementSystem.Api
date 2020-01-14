using System;
using System.Net;
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
        /// <param name="grantType">password or refresh_token</param>
        /// <param name="login">Email</param>
        /// <param name="password">Password</param>
        /// <param name="refreshToken">Refresh token</param>
        [HttpPost("oauth/token")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        public async Task<IActionResult> Authentication(CancellationToken cancellationToken,
            [FromForm(Name = "grant_type")] string grantType,
            [FromForm(Name = "login")] string login,
            [FromForm(Name = "password")] string password,
            [FromForm(Name = "refresh_token")] string refreshToken,
            [FromServices] UserAuthenticationService authenticationService)
        {
            const string passwordGrantType = "password";
            const string refreshTokenGrantType = "refresh_token";

            if (string.IsNullOrEmpty(grantType))
                throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.GrantTypeIsEmpty,
                    "Grant type cannot be empty");
            
            switch (grantType)
            {
                case passwordGrantType:
                    try
                    {
                        if (string.IsNullOrEmpty(login))
                            throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.LoginIsEmpty,
                                $"Login cannot be empty for grant type '{passwordGrantType}'");

                        if (string.IsNullOrEmpty(password))
                            throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.PasswordIsEmpty,
                                $"Password cannot be empty for grant type '{passwordGrantType}'");

                        var token = await authenticationService.AuthenticationByPassword(login,
                            password, cancellationToken);
                        
                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        throw new ApiException(HttpStatusCode.Unauthorized, ErrorCode.InvalidCredentials,
                            "Login or password is invalid");
                    }
                case refreshTokenGrantType:
                    try
                    {
                        if (string.IsNullOrEmpty(refreshToken))
                            throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.RefreshTokenIsEmpty,
                                $"Refresh token cannot empty for grant type '{refreshTokenGrantType}'");

                        if (Guid.TryParse(refreshToken, out var normalizedRefreshToken) == false)
                            throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.InvalidRefreshToken,
                                $"Invalid refresh token");

                        var token = await authenticationService.AuthenticationByRefreshToken(normalizedRefreshToken,
                            cancellationToken);
                        
                        return Ok(new TokenView(token.AccessToken, token.ExpiresIn, token.RefreshToken));
                    }
                    catch (InvalidCredentialsException)
                    {
                        throw new ApiException(HttpStatusCode.Unauthorized, ErrorCode.InvalidCredentials,
                            "Refresh token not found");
                    }
                default:
                    throw new ApiException(HttpStatusCode.BadRequest, ErrorCode.UnsupportedGrantType,
                        "Unsupported grant type");
            }
        }
    }
}