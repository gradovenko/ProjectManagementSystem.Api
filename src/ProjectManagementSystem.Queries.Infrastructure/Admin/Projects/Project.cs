namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects;

internal sealed class Project
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsPrivate { get; }
    public ProjectStatus Status { get; }
    public DateTime CreateDate { get; }
}