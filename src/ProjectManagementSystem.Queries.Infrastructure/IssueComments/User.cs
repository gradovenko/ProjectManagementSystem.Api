
namespace ProjectManagementSystem.Queries.Infrastructure.IssueComments;

internal sealed record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
}