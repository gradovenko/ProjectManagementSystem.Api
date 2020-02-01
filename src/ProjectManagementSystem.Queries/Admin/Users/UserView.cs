using System;

namespace ProjectManagementSystem.Queries.Admin.Users
{
    public sealed class UserView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}