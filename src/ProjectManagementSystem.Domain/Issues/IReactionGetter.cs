namespace ProjectManagementSystem.Domain.Issues;

public interface IReactionGetter
{
    Task<Reaction?> Get(Guid id, CancellationToken cancellationToken);
}