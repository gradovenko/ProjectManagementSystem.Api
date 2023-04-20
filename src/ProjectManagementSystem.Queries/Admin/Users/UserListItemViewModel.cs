namespace ProjectManagementSystem.Queries.Admin.Users;

public sealed record UserListItemViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Role { get; init; } = null!;
    public string State { get; init; } = null!;
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
}