using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses
{
    public class IssueStatus
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool IsActive { get; }
    }
}