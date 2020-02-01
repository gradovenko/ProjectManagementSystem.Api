using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class IssuePriority
    {
        public Guid IssuePriorityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}