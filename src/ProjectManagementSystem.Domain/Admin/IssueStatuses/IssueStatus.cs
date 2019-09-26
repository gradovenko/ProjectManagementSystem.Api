using System;

namespace ProjectManagementSystem.Domain.Admin.IssueStatuses
{
    public class IssueStatus
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public IssueStatus(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }
    }
}