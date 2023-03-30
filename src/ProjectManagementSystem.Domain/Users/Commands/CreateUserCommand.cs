using MediatR;

namespace ProjectManagementSystem.Domain.Users.Commands;

public sealed record CreateUserCommand : IRequest<CreateUserCommandResultState>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public UserRole Role { get; init; }
}

public enum CreateUserCommandResultState
{
    UserCreated,
    UserWithSameIdButOtherParamsAlreadyExists,
    UserWithSameNameAlreadyExists,
    UserWithSameEmailAlreadyExists,
}