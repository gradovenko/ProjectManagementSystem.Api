namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore;

public sealed class RefreshToken
{
    public string Id { get; }
    public DateTime ExpireDate { get; private set; }
    public Guid UserId { get; }

    private RefreshToken() { }

    public RefreshToken(string id, TimeSpan expiresIn, Guid userId)
    {
        Id = id;
        ExpireDate = DateTime.UtcNow.Add(expiresIn);;
        UserId = userId;
    }
        
    public void Terminate()
    {
        ExpireDate = DateTime.UtcNow;
    }
}