using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class MemberRole
    {
        public Guid MemberId { get; set; }
        public Guid RoleId { get; set; }
    }
}