using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class ReactionGetter : IReactionGetter
{
    private readonly IssueDbContext _context;

    public ReactionGetter(IssueDbContext context)
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