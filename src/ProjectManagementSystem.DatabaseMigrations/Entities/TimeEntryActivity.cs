using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class TimeEntryActivity
    {
        public Guid TimeEntryActivityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}