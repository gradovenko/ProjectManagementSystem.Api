using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Members;

namespace ProjectManagementSystem.Infrastructure.User.Members
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly MemberDbContext _context;

        public UserRepository(MemberDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.User.Members.User> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}