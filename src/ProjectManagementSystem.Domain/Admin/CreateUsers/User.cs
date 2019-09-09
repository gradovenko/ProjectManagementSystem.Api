using System;

namespace ProjectManagementSystem.Domain.Admin.CreateUsers
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; private set; }
        public UserStatus Status { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Guid ConcurrencyStamp { get; set; } = Guid.NewGuid();

        public User(Guid id, string name, string email, string passwordHash, string firstName, string lastName, UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreateDate = DateTime.UtcNow;
            Status = UserStatus.Active;
        }
    }
}