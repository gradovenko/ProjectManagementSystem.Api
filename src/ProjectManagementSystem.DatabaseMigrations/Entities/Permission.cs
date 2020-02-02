using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Permission
    {
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
    }
}