namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

internal sealed class Project
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = null!;
}