using System;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Accounts
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
    }
}