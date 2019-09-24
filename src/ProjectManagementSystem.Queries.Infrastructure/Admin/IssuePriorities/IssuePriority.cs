using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePriority
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
    }
}