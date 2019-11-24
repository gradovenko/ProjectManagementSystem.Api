using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}