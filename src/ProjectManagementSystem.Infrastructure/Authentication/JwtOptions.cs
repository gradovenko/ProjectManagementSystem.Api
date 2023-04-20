namespace ProjectManagementSystem.Infrastructure.Authentication;

public sealed class JwtAuthOptions
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public int LifeTimeInMinutes { get; set; }
}