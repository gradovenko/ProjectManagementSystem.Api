using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Member
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}