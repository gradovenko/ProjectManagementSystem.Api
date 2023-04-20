namespace ProjectManagementSystem.Domain.Comments;

public interface IReactionGetter
{
    Task<Reaction?> Get(Guid id, CancellationToken cancellationToken);
}