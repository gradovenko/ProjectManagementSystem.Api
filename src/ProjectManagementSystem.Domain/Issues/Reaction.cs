namespace ProjectManagementSystem.Domain.Issues;

public sealed record Reaction
{
    public string Id { get; private set; } = null!;
    // public IEnumerable<Issue> Issues { get; private set; } = null!;
}