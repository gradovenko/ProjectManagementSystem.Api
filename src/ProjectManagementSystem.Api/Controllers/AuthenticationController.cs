using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Models.Authentication;
using ProjectManagementSystem.Domain.Authentication.Commands;

namespace ProjectManagementSystem.Api.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class AuthenticationController : ControllerBase
{
    /// <summary>
    /// Camera owner authentication, grant_type = (password, authorization_code, refresh_token)
    /// </summary>
    /// <param name="model">Authentication model</param>
    /// <response code="200">Successfully</response>
    /// <response code="400">Bad request</response>
    [HttpPost("/oauth2/token")]
    [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Authentication(
        CancellationToken cancellationToken,
        [FromForm] AuthenticateModel model,
        [FromServices] IMediator mediator)
    {
        const string passwordGrantType = "password";
        const string refreshTokenGrantType = "refresh_token";

        if (string.IsNullOrEmpty(model.GrantType))
            return BadRequest(new ErrorViewModel(ErrorCode.InvalidRequest, "Field 'grant_type' is required"));
            
        switch (model.GrantType)
        {
            case passwordGrantType:
                if (string.IsNullOrEmpty(model.UserName))
                    BadRequest(new ErrorViewModel(ErrorCode.InvalidRequest,
                        $"Field 'username' is required for '{passwordGrantType}' grant type"));

                if (string.IsNullOrEmpty(model.Password))
                    BadRequest(new ErrorViewModel(ErrorCode.InvalidRequest,
                        $"Field 'password' is required for '{passwordGrantType}' grant type"));
                
                var authenticateUserByPasswordCommandResult = await mediator.Send(new AuthenticateUserByPasswordCommand
                {
                    Login = model.UserName!,
                    Password = model.Password!
                }, cancellationToken);

                return authenticateUserByPasswordCommandResult.State switch
                {
                    AuthenticateUserByPasswordCommandResultState.PasswordNotValid or AuthenticateUserByPasswordCommandResultState.UserNotFound => 
                        BadRequest(new ErrorViewModel(ErrorCode.UnauthorizedClient, "Username or password is incorrect")),
                    AuthenticateUserByPasswordCommandResultState.Authenticated => 
                        Ok(new TokenViewModel(authenticateUserByPasswordCommandResult.Value!.AccessToken, authenticateUserByPasswordCommandResult.Value!.ExpiresIn, authenticateUserByPasswordCommandResult.Value!.RefreshToken)),
                    _ => throw new ArgumentOutOfRangeException()
                };

            case refreshTokenGrantType:
                if (string.IsNullOrEmpty(model.RefreshToken))
                    BadRequest(new ErrorViewModel(ErrorCode.InvalidRequest,
                        $"Field 'refresh_token' is required for '{refreshTokenGrantType}' grant type"));

                var authenticateUserByRefreshTokenCommandResult = await mediator.Send(new AuthenticateUserByRefreshTokenCommand
                {
                    RefreshToken = model.RefreshToken!
                }, cancellationToken);

                return authenticateUserByRefreshTokenCommandResult.State switch
                {
                    AuthenticateUserByRefreshTokenCommandResultState.OldRefreshTokenNotFound => 
                        BadRequest(new ErrorViewModel(ErrorCode.UnauthorizedClient, "Username or password is incorrect")),
                    AuthenticateUserByRefreshTokenCommandResultState.Authenticated => 
                        Ok(new TokenViewModel(authenticateUserByRefreshTokenCommandResult.Value!.AccessToken, authenticateUserByRefreshTokenCommandResult.Value!.ExpiresIn, authenticateUserByRefreshTokenCommandResult.Value!.RefreshToken)),
                    _ => throw new ArgumentOutOfRangeException()
                };

            default:
                return BadRequest(new ErrorViewModel(ErrorCode.UnsupportedGrantType,
                    $"Unsupported grant type: {model.GrantType}. Possible types: {refreshTokenGrantType}"));
        }
    }
}