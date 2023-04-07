using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.TimeEntries;

public sealed class IssueGetter : IIssueGetter
{
    private readonly TimeEntryDbContext _context;

    public IssueGetter(TimeEntryDbContext context)
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