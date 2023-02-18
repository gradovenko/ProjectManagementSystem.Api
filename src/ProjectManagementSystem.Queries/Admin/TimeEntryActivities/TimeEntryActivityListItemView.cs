namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

public sealed record TimeEntryActivityListItemView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}