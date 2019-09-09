using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}