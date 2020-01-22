using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses
{
    internal sealed class IssueStatus
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool IsActive { get; }
    }
}