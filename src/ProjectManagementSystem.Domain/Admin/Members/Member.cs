using System;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public sealed class Member
    {
        public Guid MemberId { get; }
        public Guid UserId { get; }
        public Guid ProjectId { get; }
        public Guid RoleId { get; }

        public Member(Guid memberId, Guid userId, Guid projectId, Guid roleId)
        {
            MemberId = memberId;
            UserId = userId;
            ProjectId = projectId;
            RoleId = roleId;
        }
    }
}