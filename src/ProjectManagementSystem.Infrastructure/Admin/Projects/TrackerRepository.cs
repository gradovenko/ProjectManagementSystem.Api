using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.Projects;

namespace ProjectManagementSystem.Infrastructure.Admin.Projects;

public sealed class TrackerRepository : ITrackerRepository
{
    private readonly ProjectDbContext _context;

    public TrackerRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<Tracker> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Trackers
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}