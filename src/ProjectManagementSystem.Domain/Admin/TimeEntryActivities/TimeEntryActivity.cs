namespace ProjectManagementSystem.Domain.Admin.TimeEntryActivities;

public sealed class TimeEntryActivity
{
    public Guid Id { get; }
    public string Name { get; }
    public bool IsActive { get; }
    private Guid _concurrencyStamp;

    public TimeEntryActivity(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
        _concurrencyStamp = Guid.NewGuid();
    }
}