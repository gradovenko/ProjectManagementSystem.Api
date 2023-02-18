using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Infrastructure.Admin.TimeEntryActivities;

public sealed class TimeEntryActivityRepository : ITimeEntryActivityRepository
{
    private readonly TimeEntryActivityDbContext _context;

    public TimeEntryActivityRepository(TimeEntryActivityDbContext context)
    {
        _context = context;
    }

    public async Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TimeEntryActivities
            .AsNoTracking()
            .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
    }

    public async Task Save(TimeEntryActivity timeEntryActivity)
    {
        if (_context.Entry(timeEntryActivity).State == EntityState.Detached)
            await _context.TimeEntryActivities.AddAsync(timeEntryActivity);

        await _context.SaveChangesAsync();
    }
}