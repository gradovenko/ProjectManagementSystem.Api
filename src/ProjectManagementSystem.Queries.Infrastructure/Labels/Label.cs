namespace ProjectManagementSystem.Queries.Infrastructure.Labels;

internal sealed record Label
{
    public Guid LabelId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
}