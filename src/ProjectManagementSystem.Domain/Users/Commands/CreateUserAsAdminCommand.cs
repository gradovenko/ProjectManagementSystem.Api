using MediatR;

namespace ProjectManagementSystem.Domain.Users.Commands;

public sealed record CreateUserAsAdminCommand : IRequest<CreateUserAsAdminCommandResultState>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public UserRole Role { get; init; }
}

public enum CreateUserAsAdminCommandResultState
{
    UserWithSameNameAlreadyExists,
    UserWithSameEmailAlreadyExists,
    UserWithSameIdButDifferentParamsAlreadyExists,
    UserCreated
}