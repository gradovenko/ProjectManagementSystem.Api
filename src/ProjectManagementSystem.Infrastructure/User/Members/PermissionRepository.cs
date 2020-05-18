using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Members;

namespace ProjectManagementSystem.Infrastructure.User.Members
{
    public sealed class PermissionRepository : IPermissionRepository
    {
        private readonly MemberDbContext _context;

        public PermissionRepository(MemberDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> Get(string id, CancellationToken cancellationToken)
        {
            return await _context.Permissions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}