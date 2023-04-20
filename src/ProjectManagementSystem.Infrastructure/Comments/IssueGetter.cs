using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class IssueGetter : IIssueGetter
{
    private readonly CommentDbContext _context;

    public IssueGetter(CommentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Issue?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Issues
            .AsNoTracking()
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}