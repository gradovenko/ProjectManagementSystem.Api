namespace ProjectManagementSystem.Domain.Admin.Users
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}