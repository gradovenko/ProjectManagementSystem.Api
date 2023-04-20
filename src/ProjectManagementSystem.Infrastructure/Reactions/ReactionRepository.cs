using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Reactions;

namespace ProjectManagementSystem.Infrastructure.Reactions;

public sealed class ReactionRepository : IReactionRepository
{
    private readonly ReactionDbContext _context;

    public ReactionRepository(ReactionDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Reaction?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Reactions
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Reaction?> GetByEmoji(string unicode, CancellationToken cancellationToken)
    {
        return await _context.Reactions
            .SingleOrDefaultAsync(o => o.Emoji == unicode, cancellationToken);
    }

    public async Task<Reaction?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Reactions
            .SingleOrDefaultAsync(o => o.Name == name, cancellationToken);
    }

    public async Task Save(Reaction reaction, CancellationToken cancellationToken)
    {
        if (_context.Entry(reaction).State == EntityState.Detached)
            await _context.Reactions.AddAsync(reaction, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}