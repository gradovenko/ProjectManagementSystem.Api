namespace ProjectManagementSystem.Queries.Infrastructure.User.Projects;

internal sealed class Project
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsPrivate { get; }
    public ProjectStatus Status { get; }
    public DateTime CreateDate { get; }
    public DateTime? UpdateDate { get; }
}