using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly IssueDbContext _context;

        public TimeEntryRepository(IssueDbContext context)
        {
            _context = context;
        }
        
        public async Task<TimeEntry> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.TimeEntries
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);        
        }
    }
}