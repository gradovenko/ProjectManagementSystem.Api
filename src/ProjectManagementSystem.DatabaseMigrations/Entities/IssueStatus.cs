using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public class IssueStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}