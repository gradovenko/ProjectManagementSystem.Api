using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users
{
    public class UsersQueryHandler : IQueryHandler<UsersQuery, Page<FullUserView>>
    {
        private readonly UserDbContext _context;

        public UsersQueryHandler(UserDbContext context)
        {
            _context = context;
        }
        
        public async Task<Page<FullUserView>> ExecuteQueryAsync(UsersQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Users.AsNoTracking()
                .Select(user => new FullUserView
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });

            return new Page<FullUserView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}