using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users
{
    public class UserQueryHandler : IRequestHandler<UserQuery, ShortUserView>
    {
        private readonly UserDbContext _context;

        public UserQueryHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUserView> Handle(UserQuery query, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Where(user => user.Id == query.Id)
                .Select(user => new ShortUserView
                {
                    Name = user.Name,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}