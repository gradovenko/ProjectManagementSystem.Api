namespace ProjectManagementSystem.Queries.User.Projects;

public sealed record ProjectListItemViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string Path { get; init; } = null!;
    public string Visibility { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
}