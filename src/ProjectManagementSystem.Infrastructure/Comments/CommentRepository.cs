using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class CommentRepository : ICommentRepository
{
    private readonly CommentDbContext _context;

    public CommentRepository(CommentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Comment?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task Save(Comment comment, CancellationToken cancellationToken)
    {
        if (_context.Entry(comment).State == EntityState.Detached)
            await _context.Comments.AddAsync(comment, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}