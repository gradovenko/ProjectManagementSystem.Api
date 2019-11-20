using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public class TimeEntryActivityRepository : ITimeEntryActivityRepository
    {
        private readonly TimeEntryDbContext _context;

        public TimeEntryActivityRepository(TimeEntryDbContext context)
        {
            _context = context;
        }
        
        public Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}