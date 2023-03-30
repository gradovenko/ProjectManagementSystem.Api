namespace ProjectManagementSystem.Domain.Issues;

public sealed record Label
{
    public Guid Id { get; private set; }
    // public IEnumerable<Issue> Issues { get; private set; } = null!;
}