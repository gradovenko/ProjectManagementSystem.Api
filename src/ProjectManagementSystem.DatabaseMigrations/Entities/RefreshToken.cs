namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record RefreshToken
{
    public string RefreshTokenId { get; init; } = null!;
    public DateTime ExpireDate { get; init; }
    public Guid UserId { get; init; }
}