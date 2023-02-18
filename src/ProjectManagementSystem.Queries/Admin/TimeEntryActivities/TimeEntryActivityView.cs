namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

public sealed record TimeEntryActivityView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}