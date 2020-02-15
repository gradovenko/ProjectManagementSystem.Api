using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.Members;

namespace ProjectManagementSystem.Infrastructure.Admin.Members
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.Admin.Members.User> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task Save(Domain.Admin.Members.User user)
        {
            if (_context.Entry(user).State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }
    }
}