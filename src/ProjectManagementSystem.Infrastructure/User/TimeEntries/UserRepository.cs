using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IssueDbContext _context;

        public UserRepository(IssueDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.User.TimeEntries.User> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}