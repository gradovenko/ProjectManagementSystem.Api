using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateUsers;

namespace ProjectManagementSystem.Infrastructure.Admin.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
        
        public async Task<User> FindByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }

        public async Task<User> FindByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task Save(User user)
        {
            if (_context.Entry(user).State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();        
        }
    }
}