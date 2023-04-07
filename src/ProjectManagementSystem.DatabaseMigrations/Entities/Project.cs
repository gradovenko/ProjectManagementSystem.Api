namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Project
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string Path { get; init; } = null!;
    public string Visibility { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public Guid ConcurrencyToken { get; init; }
    public IEnumerable<Issue> Issues { get; init; } = null!;
}