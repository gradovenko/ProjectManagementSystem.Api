using System;

namespace ProjectManagementSystem.Domain.User.Accounts
{
    public class User
    {
        protected User() { }
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        private Guid _concurrencyStamp = Guid.NewGuid();

        internal void UpdateName(string name)
        {
            UserName = name;
            UpdateDate = DateTime.UtcNow;
            _concurrencyStamp = Guid.NewGuid();
        }
        
        internal void UpdateEmail(string email)
        {
            Email = email;
            UpdateDate = DateTime.UtcNow;
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}