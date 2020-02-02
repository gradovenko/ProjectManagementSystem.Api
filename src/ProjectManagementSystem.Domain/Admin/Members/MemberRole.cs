using System;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public sealed class MemberRole
    {
        public Guid MemberId { get; }
        public Guid UserId { get; }
    }
}