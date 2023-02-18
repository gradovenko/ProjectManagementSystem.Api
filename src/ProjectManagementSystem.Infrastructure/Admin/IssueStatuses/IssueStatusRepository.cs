using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;

namespace ProjectManagementSystem.Infrastructure.Admin.IssueStatuses;

public sealed class IssueStatusRepository : IIssueStatusRepository
{
    private readonly IssueStatusDbContext _context;

    public IssueStatusRepository(IssueStatusDbContext context)
    {
        _context = context;
    }
        
    public async Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.IssueStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
    }

    public async Task Save(IssueStatus issueStatus)
    {
        if (_context.Entry(issueStatus).State == EntityState.Detached)
            await _context.IssueStatuses.AddAsync(issueStatus);

        await _context.SaveChangesAsync();  
    }
}