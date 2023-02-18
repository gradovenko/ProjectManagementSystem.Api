namespace ProjectManagementSystem.Domain.Authentication;

public sealed class RefreshToken
{
    public string Value { get; }

    public Guid UserId { get; }

    public RefreshToken(string value, Guid userId)
    {
        Value = value;
        UserId = userId;
    }
}