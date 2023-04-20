using MediatR;

namespace ProjectManagementSystem.Domain.Users.Commands;

public sealed record UpdateUserAsAdminCommand : IRequest<UpdateUserAsAdminCommandResultState>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    //public string Email { get; init; } = null!;
    //public UserRole Role { get; init; }
}

public enum UpdateUserAsAdminCommandResultState
{
    UserNotFound,
    UserWithSameNameAlreadyExists,
    UserWithSameEmailAlreadyExists,
    UserUpdated
}