using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly TimeEntryDbContext _context;

        public UserRepository(TimeEntryDbContext context)
        {
            _context = context;
        }
        
        public Task<Domain.User.TimeEntries.User> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}