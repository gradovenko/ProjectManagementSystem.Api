using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public class Tracker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}