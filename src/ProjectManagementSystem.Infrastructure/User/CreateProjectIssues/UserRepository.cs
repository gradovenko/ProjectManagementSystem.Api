using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ProjectDbContext _context;

        public UserRepository(ProjectDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.User.CreateProjectIssues.User> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}