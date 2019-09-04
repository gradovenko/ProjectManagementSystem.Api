namespace ProjectManagementSystem.Domain.Admin.CreateUsers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}