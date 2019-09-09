using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.Authentication
{
    public class UserRepository : Domain.Authentication.IUserRepository
    {
        private readonly AuthenticationDbContext _context;

        public UserRepository(AuthenticationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.Authentication.User> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
        
        public async Task<Domain.Authentication.User> FindByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }

        public async Task<Domain.Authentication.User> FindByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}