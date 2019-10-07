using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}