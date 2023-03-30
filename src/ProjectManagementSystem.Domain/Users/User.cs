namespace ProjectManagementSystem.Domain.Users;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public UserState State { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    private Guid _concurrencyToken;

    public User(Guid id, string name, string email, string passwordHash, UserRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        State = UserState.Active;
        CreateDate = UpdateDate =  DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string name)
    {
        Name = name;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}