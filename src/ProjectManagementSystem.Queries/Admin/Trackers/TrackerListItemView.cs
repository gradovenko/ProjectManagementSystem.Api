namespace ProjectManagementSystem.Queries.Admin.Trackers;

public sealed record TrackerListItemView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}