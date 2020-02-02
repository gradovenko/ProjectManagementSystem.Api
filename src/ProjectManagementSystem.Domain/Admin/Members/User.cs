using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public sealed class User
    {
        public Guid Id { get; }
        private List<Member> _members = new List<Member>();
        public IEnumerable<Member> Members => _members;
        private Guid _concurrencyStamp;

        public void AddMember(Member member)
        {
            _members.Add(member);
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}