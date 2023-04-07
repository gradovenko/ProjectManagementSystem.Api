using MediatR;

namespace ProjectManagementSystem.Domain.Users.Commands;

public sealed class ChangeUserPasswordViaOldPasswordCommand : IRequest<ChangeUserPasswordViaOldPasswordCommandResultState>
{
    public Guid UserId { get; init; }
    public string OldPassword { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
}

public enum ChangeUserPasswordViaOldPasswordCommandResultState
{
    UserNotFound,
    UserPasswordWrong,
    UserPasswordChanged
}