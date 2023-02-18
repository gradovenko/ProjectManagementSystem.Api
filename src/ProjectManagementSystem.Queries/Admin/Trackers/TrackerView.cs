namespace ProjectManagementSystem.Queries.Admin.Trackers;

public sealed record TrackerView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}