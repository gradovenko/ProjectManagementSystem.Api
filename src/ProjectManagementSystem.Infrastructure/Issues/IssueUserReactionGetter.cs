using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueUserReactionGetter : IIssueUserReactionGetter
{
    private readonly IssueDbContext _context;

    public IssueUserReactionGetter(IssueDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IssueUserReaction?> Get(Guid issueId, Guid userId, Guid reactionId,
        CancellationToken cancellationToken)
    {
        return await _context.IssueUserReactions
            .AsNoTracking()
            .SingleOrDefaultAsync(o => o.IssueId == issueId && o.UserId == userId && o.ReactionId == reactionId,
                cancellationToken);
    }
}