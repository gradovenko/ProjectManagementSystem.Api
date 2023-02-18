namespace ProjectManagementSystem.Domain.User.Accounts;

public interface IPasswordHasher
{
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}