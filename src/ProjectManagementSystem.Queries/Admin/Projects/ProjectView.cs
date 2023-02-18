namespace ProjectManagementSystem.Queries.Admin.Projects;

public sealed record ProjectView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreateDate { get; set; }
}