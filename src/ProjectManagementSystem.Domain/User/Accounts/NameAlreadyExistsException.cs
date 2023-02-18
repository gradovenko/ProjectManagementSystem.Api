namespace ProjectManagementSystem.Domain.User.Accounts;

public sealed class NameAlreadyExistsException : Exception
{
    public NameAlreadyExistsException() { }
}