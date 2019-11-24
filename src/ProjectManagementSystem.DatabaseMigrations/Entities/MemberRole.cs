using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class MemberRole
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}