using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.Admin.Users
{
    public sealed class UserRepository : Domain.Admin.CreateUsers.IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.Admin.CreateUsers.User> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
        
        public async Task<Domain.Admin.CreateUsers.User> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<Domain.Admin.CreateUsers.User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task Save(Domain.Admin.CreateUsers.User user)
        {
            if (_context.Entry(user).State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();        
        }
    }
}