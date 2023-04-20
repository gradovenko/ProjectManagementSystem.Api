using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Reactions;

public sealed class ReactionQueryHandler :
    IRequestHandler<Queries.User.Reactions.ReactionListQuery, IEnumerable<Queries.User.Reactions.ReactionListItemViewModel>>,
    IRequestHandler<Queries.Admin.Reactions.ReactionListQuery, Page<Queries.Admin.Reactions.ReactionListItemViewModel>>
{
    private readonly ReactionQueryDbContext _context;

    public ReactionQueryHandler(ReactionQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Queries.User.Reactions.ReactionListItemViewModel>> Handle(
        Queries.User.Reactions.ReactionListQuery request, CancellationToken cancellationToken)
    { 
        return await _context.Reactions.AsNoTracking()
            .Select(o => new Queries.User.Reactions.ReactionListItemViewModel
            {
                Id = o.ReactionId,
                Emoji = o.Emoji,
                Name = o.Name,
                Category = o.Category
            }).ToListAsync(cancellationToken);
    }

    public async Task<Page<Queries.Admin.Reactions.ReactionListItemViewModel>> Handle(Queries.Admin.Reactions.ReactionListQuery request,
        CancellationToken cancellationToken)
    {
        var sql = _context.Reactions.AsNoTracking()
            .Select(o => new Queries.Admin.Reactions.ReactionListItemViewModel
            {
                Id = o.ReactionId,
                Emoji = o.Emoji,
                Name = o.Name,
                Category = o.Category
            });

        return new Page<Queries.Admin.Reactions.ReactionListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}