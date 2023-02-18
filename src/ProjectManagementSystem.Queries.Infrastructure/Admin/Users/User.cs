namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users;

internal sealed class User
{
    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
}