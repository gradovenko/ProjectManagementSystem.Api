namespace ProjectManagementSystem.Domain.Users;

public sealed class UserCreator
{
    private readonly IPasswordHasher _passwordHasher;

    public UserCreator(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public User CreateUser(Guid id, string name, string email, string password, UserRole role)
    {
        string passwordHash = _passwordHasher.HashPassword(password);
        return new User(id, name, email, passwordHash, role);
    }
}