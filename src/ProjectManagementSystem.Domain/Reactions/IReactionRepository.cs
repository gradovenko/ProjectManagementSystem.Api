namespace ProjectManagementSystem.Domain.Reactions;

public interface IReactionRepository
{
    Task<Reaction?> Get(Guid id, CancellationToken cancellationToken);
    Task<Reaction?> GetByEmoji(string emoji, CancellationToken cancellationToken);
    Task<Reaction?> GetByName(string name, CancellationToken cancellationToken);
    Task Save(Reaction reaction, CancellationToken cancellationToken);
}