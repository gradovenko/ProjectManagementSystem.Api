namespace ProjectManagementSystem.Queries.Admin.IssueStatuses;

public sealed record IssueStatusView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}