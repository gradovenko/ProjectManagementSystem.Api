using System;

namespace ProjectManagementSystem.Domain.Authentication
{
    public sealed class RefreshToken
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