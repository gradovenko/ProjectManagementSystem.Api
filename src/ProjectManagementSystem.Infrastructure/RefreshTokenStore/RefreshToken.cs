using System;

namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public DateTime ExpireDate { get; private set; }
        
        public Guid UserId { get; private set; }

        public RefreshToken(Guid id, TimeSpan expiresIn, Guid userId)
        {
            Id = id;
            ExpireDate = DateTime.UtcNow.Add(expiresIn);;
            UserId = userId;
        }

        protected RefreshToken() { }

        public void Terminate()
        {
            ExpireDate = DateTime.UtcNow;
        }
    }
}