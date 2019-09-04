using System;

namespace ProjectManagementSystem.Domain.Authentication
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
    }
}