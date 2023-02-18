namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public sealed class Tracker
{
    public Guid TrackerId { get; set; }
    public string Name { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}