namespace ProjectManagementSystem.Domain.Users;

public interface IPasswordHasher
{
    string HashPassword(string password);
}