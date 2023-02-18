namespace ProjectManagementSystem.Domain.Admin.IssuePriorities;

public sealed class IssuePriority
{
    public Guid Id { get; }
    public string Name { get; }
    public bool IsActive { get; }

    public IssuePriority(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }
}