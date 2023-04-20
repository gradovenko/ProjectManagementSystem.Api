using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class CommentUserReactionGetter : ICommentUserReactionGetter
{
    private readonly CommentDbContext _context;

    public CommentUserReactionGetter(CommentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CommentUserReaction?> Get(Guid commentId, Guid userId, Guid reactionId,
        CancellationToken cancellationToken)
    {
        return await _context.CommentUserReactions
            .AsNoTracking()
            .SingleOrDefaultAsync(o => o.CommentId == commentId && o.UserId == userId && o.ReactionId == reactionId,
                cancellationToken);
    }
}