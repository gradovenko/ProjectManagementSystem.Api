using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users;

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
                FirstName = user.FirstName,
                LastName = user.LastName,
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}