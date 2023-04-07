namespace ProjectManagementSystem.Queries.Infrastructure.Users;

internal sealed record User
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
}