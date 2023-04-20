using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueRepository : IIssueRepository
{
    private readonly IssueDbContext _context;

    public IssueRepository(IssueDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Issue?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Issues
            .Include(o => o.IssueAssignees)
            .Include(o => o.IssueLabels)
            .Include(o => o.IssueUserReactions)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task Save(Issue issue, CancellationToken cancellationToken)
    {
        if (_context.Entry(issue).State == EntityState.Detached)
            _context.Issues.Add(issue);

        await _context.SaveChangesAsync(cancellationToken); 
    }
}