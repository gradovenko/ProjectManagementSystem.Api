using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.TimeEntries;

public sealed class TimeEntryRepository : ITimeEntryRepository
{
    private readonly TimeEntryDbContext _context;

    public TimeEntryRepository(TimeEntryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TimeEntry?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TimeEntries
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);        
    }

    public async Task Save(TimeEntry timeEntry, CancellationToken cancellationToken)
    {
        if (_context.Entry(timeEntry).State == EntityState.Detached)
            await _context.TimeEntries.AddAsync(timeEntry, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);        
    }
}