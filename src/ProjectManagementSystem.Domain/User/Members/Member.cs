using System;

namespace ProjectManagementSystem.Domain.User.Members
{
    public sealed class Member
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Member(Guid userId, Guid projectId, Guid roleId)
        {
            UserId = userId;
            ProjectId = projectId;
            RoleId = roleId;
        }
    }
}