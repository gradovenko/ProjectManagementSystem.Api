using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.User.Members
{
    public sealed class Member
    {
        public Guid MemberId { get; }
        public Guid UserId { get; }
        public Guid ProjectId { get; }
        public Guid RoleId { get; }
        private List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IEnumerable<RolePermission> RolePermissions => _rolePermissions;

        public Member(Guid memberId, Guid userId, Guid projectId, Guid roleId)
        {
            MemberId = memberId;
            UserId = userId;
            ProjectId = projectId;
            RoleId = roleId;
        }
    }
}