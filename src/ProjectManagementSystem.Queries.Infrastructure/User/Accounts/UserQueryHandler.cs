using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Accounts;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Accounts
{
    public class UserQueryHandler : IQueryHandler<UserQuery, UserView>
    {
        private readonly UserDbContext _context;

        public UserQueryHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserView> ExecuteQueryAsync(UserQuery query, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Where(user => user.Id == query.Id)
                .Select(user => new UserView
                {
                    UserName = user.UserName,
                    Email = user.Email,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}