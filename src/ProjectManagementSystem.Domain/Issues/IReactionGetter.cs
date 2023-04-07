namespace ProjectManagementSystem.Domain.Issues;

public interface IReactionGetter
{
    Task<Reaction?> Get(string id, CancellationToken cancellationToken);
}