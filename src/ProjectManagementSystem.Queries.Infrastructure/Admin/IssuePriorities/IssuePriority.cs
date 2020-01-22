using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    internal sealed class IssuePriority
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool IsActive { get; }
    }
}