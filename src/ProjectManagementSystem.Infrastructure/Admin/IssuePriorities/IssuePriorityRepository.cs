using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.IssuePriorities;

namespace ProjectManagementSystem.Infrastructure.Admin.IssuePriorities;

public sealed class IssuePriorityRepository : IIssuePriorityRepository
{
    private readonly IssuePriorityDbContext _context;

    public IssuePriorityRepository(IssuePriorityDbContext context)
    {
        _context = context;
    }

    public async Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.IssuePriorities
            .AsNoTracking()
            .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
    }

    public async Task Save(IssuePriority issuePriority)
    {
        if (_context.Entry(issuePriority).State == EntityState.Detached)
            await _context.IssuePriorities.AddAsync(issuePriority);

        await _context.SaveChangesAsync();        
    }
}