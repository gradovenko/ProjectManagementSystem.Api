using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateTrackers;

namespace ProjectManagementSystem.Infrastructure.Admin.CreateTrackers
{
    public sealed class TrackerRepository : ITrackerRepository
    {
        private readonly TrackerDbContext _context;

        public TrackerRepository(TrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Tracker> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Trackers
                .AsNoTracking()
                .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
        }

        public async Task Save(Tracker tracker)
        {
            if (_context.Entry(tracker).State == EntityState.Detached)
                await _context.Trackers.AddAsync(tracker);

            await _context.SaveChangesAsync();  
        }
    }
}