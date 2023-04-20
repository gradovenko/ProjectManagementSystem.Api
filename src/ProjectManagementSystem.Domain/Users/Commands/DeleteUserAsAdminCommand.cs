using MediatR;

namespace ProjectManagementSystem.Domain.Users.Commands;

public sealed record DeleteUserAsAdminCommand : IRequest<DeleteUserAsAdminCommandResultState>
{
    public Guid UserId { get; init; }
}

public enum DeleteUserAsAdminCommandResultState
{
    UserNotFound,
    UserDeleted
}