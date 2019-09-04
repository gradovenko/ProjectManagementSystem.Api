using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public DateTime ExpireDate { get; set; }
        
        public Guid UserId { get; set; }
    }
}