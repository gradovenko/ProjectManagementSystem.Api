namespace ProjectManagementSystem.Domain.Admin.Users;

public sealed class User
{
    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserRole Role { get; }
    public UserStatus Status { get; }
    public DateTime CreateDate { get; }
    private Guid _concurrencyStamp;

    public User(Guid id, string name, string email, string passwordHash, string firstName, string lastName, UserRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        CreateDate = DateTime.UtcNow;
        Status = UserStatus.Active;
        _concurrencyStamp = Guid.NewGuid();
    }
}