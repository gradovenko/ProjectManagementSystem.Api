namespace ProjectManagementSystem.Domain.Comments;

public interface IUserGetter
{
    Task<User?> Get(Guid id, CancellationToken cancellationToken);
}