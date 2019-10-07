using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}