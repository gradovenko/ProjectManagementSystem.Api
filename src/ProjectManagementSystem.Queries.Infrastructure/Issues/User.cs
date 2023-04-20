namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

internal sealed record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public IEnumerable<Issue> Issues { get; init; } = null!;
}