using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Accounts;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Accounts
{
    public sealed class UserQueryHandler : IRequestHandler<UserQuery, UserView>
    {
        private readonly UserDbContext _context;

        public UserQueryHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserView> Handle(UserQuery query, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Where(user => user.Id == query.Id)
                .Select(user => new UserView
                {
                    Name = user.Name,
                    Email = user.Email,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}