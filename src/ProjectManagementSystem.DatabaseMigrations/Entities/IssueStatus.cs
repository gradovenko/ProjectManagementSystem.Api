using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class IssueStatus
    {
        public Guid IssueStatusId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}