namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

internal sealed class Project
{
    public Guid ProjectId { get; init; }
    public IEnumerable<Issue> Issues { get; init; } = null!;
}