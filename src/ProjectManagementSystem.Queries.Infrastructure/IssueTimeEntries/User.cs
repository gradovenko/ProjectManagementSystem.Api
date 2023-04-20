
namespace ProjectManagementSystem.Queries.Infrastructure.IssueTimeEntries;

internal sealed record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
}