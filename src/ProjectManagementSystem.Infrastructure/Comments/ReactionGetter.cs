using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class ReactionGetter : IReactionGetter
{
    private readonly CommentDbContext _context;

    public ReactionGetter(CommentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Reaction?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Reactions
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}