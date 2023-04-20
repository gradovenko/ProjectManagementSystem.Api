namespace ProjectManagementSystem.Queries.Infrastructure.Users;

internal sealed record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Role { get; init; } = null!;
    public string State { get; init; } = null!;
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
}