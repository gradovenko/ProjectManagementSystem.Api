using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users
{
    public class UserQueryHandler : IQueryHandler<UserQuery, ShortUserView>
    {
        private readonly UserDbContext _context;

        public UserQueryHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUserView> ExecuteQueryAsync(UserQuery query, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Where(user => user.Id == query.Id)
                .Select(user => new ShortUserView
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}