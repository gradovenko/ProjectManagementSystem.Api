namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore;

internal sealed class RefreshToken
{
    public string Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpireDate { get; private set; }
    
    private RefreshToken() { }
    
    public RefreshToken(string id, Guid userId, TimeSpan expiresIn)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        UserId = userId;
        ExpireDate = DateTime.UtcNow.Add(expiresIn);
    }

    public void Terminate()
    {
        ExpireDate = DateTime.UtcNow;
    }
}