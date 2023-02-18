namespace ProjectManagementSystem.Domain.Admin.IssueStatuses;

public sealed class IssueStatus
{
    public Guid Id { get; }
    public string Name { get; }
    public bool IsActive { get; }

    public IssueStatus(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }
}