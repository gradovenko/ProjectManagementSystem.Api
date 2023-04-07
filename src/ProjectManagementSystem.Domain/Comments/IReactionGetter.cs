namespace ProjectManagementSystem.Domain.Comments;

public interface IReactionGetter
{
    Task<Reaction?> Get(string name, CancellationToken cancellationToken);
}