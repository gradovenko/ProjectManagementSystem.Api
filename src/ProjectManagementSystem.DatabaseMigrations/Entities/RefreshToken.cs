namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public sealed class RefreshToken
{
    public string RefreshTokenId { get; set; }
    public DateTime ExpireDate { get; set; }
    public Guid UserId { get; set; }
}