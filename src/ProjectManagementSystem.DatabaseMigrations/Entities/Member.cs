using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Member
    {
        public Guid MemberId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}