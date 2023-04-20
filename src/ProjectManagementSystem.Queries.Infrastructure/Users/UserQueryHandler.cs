using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Users;

public sealed class UserQueryHandler : 
    IRequestHandler<Queries.User.Profiles.UserQuery, Queries.User.Profiles.UserViewModel>,
    IRequestHandler<Queries.Admin.Users.UserListQuery, Page<Queries.Admin.Users.UserListItemViewModel>>
{
    private readonly UserDbContext _context;

    public UserQueryHandler(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task <Queries.User.Profiles.UserViewModel> Handle(Queries.User.Profiles.UserQuery query, CancellationToken cancellationToken)
    {
        return (await _context.Users.AsNoTracking()
            .Where(user => user.UserId == query.Id)
            .Select(user => new Queries.User.Profiles.UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
            })
            .SingleOrDefaultAsync(cancellationToken))!;
    }

    public async Task<Page<Queries.Admin.Users.UserListItemViewModel>> Handle(Queries.Admin.Users.UserListQuery request, CancellationToken cancellationToken)
    {
        var sql = _context.Users.AsNoTracking()
            .Select(o => new Queries.Admin.Users.UserListItemViewModel
            {
                Id = o.UserId,
                Name = o.Name,
                Email = o.Email,
                Role = o.Role,
                State = o.State,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate
            });

        return new Page<Queries.Admin.Users.UserListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}