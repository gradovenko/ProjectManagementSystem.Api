using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Member
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}