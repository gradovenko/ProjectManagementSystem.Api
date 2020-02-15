using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Members;

namespace ProjectManagementSystem.Infrastructure.User.Members
{
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly MemberDbContext _context;

        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.User.Members.Member> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Members
                .SingleOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }
    }
}