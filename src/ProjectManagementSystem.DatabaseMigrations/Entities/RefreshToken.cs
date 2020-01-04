using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get; set; }
    }
}