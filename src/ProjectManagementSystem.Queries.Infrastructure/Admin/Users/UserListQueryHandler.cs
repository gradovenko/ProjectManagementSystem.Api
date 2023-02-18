using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users;

public sealed class UserListQueryHandler : IRequestHandler<UserListQuery, Page<UserListItemView>>
{
    private readonly UserDbContext _context;

    public UserListQueryHandler(UserDbContext context)
    {
        _context = context;
    }
        
    public async Task<Page<UserListItemView>> Handle(UserListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Users.AsNoTracking()
            .Select(user => new UserListItemView
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            });

        return new Page<UserListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}