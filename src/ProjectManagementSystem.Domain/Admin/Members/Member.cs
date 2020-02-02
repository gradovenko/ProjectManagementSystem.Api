using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public sealed class Member
    {
        public Guid MemberId { get; }
        public Guid ProjectId { get; }
        public Guid RoleId { get; }
    }
}