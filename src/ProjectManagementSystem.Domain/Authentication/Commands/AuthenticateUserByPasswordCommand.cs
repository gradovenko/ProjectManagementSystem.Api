using MediatR;

namespace ProjectManagementSystem.Domain.Authentication.Commands;

public record AuthenticateUserByPasswordCommand : IRequest<CommandResult<Token, AuthenticateUserByPasswordCommandResultState>>
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public enum AuthenticateUserByPasswordCommandResultState
{
    Authenticated,
    UserNotFound,
    PasswordNotValid
}