namespace ProjectManagementSystem.Domain.Issues;

public sealed record User
{
    public Guid Id { get; private set; }
    public IEnumerable<Issue> Issues { get; private set; } = null!;

    private User() { }
}