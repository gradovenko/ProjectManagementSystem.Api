using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
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
}