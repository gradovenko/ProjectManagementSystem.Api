namespace ProjectManagementSystem.Domain.User.Accounts;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    private Guid _concurrencyStamp;

    private User() { }

    internal void UpdateName(string name)
    {
        Name = name;
        UpdateDate = DateTime.UtcNow;
        _concurrencyStamp = Guid.NewGuid();
    }
        
    internal void UpdateEmail(string email)
    {
        Email = email;
        UpdateDate = DateTime.UtcNow;
        _concurrencyStamp = Guid.NewGuid();
    }
}