using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.Admin.CreateTrackers;

namespace ProjectManagementSystem.Infrastructure.Admin.CreateTrackers
{
    public class TrackerRepository : ITrackerRepository
    {
        private readonly TrackerDbContext _dbContext;

        public TrackerRepository(TrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tracker> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Tracker tracker)
        {
            throw new NotImplementedException();
        }
    }
}