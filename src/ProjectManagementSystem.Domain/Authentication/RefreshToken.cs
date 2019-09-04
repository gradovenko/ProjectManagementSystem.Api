using System;

namespace ProjectManagementSystem.Domain.Authentication
{
    public class RefreshToken
    {
        public Guid Value { get; }

        public Guid UserId { get; }

        public RefreshToken(Guid value, Guid userId)
        {
            Value = value;
            UserId = userId;
        }
    }
}