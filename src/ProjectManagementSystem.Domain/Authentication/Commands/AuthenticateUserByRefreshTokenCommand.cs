using MediatR;

namespace ProjectManagementSystem.Domain.Authentication.Commands;

public record AuthenticateUserByRefreshTokenCommand : IRequest<CommandResult<Token, AuthenticateUserByRefreshTokenCommandResultState>>
{
    public string RefreshToken { get; init; } = null!;
}

public enum AuthenticateUserByRefreshTokenCommandResultState
{
    Authenticated,
    OldRefreshTokenNotFound
}