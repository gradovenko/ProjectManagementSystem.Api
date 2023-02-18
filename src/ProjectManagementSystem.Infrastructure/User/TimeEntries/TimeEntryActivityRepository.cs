using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries;

public class TimeEntryActivityRepository : ITimeEntryActivityRepository
{
    private readonly IssueDbContext _context;

    public TimeEntryActivityRepository(IssueDbContext context)
    {
        _context = context;
    }
        
    public async Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TimeEntryActivities
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}