namespace ProjectManagementSystem.Queries.Admin.IssuePriorities;

public sealed record IssuePriorityView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}