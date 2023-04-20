using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.IssueReactions;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueReactions;

public sealed class ReactionQueryHandler : IRequestHandler<ReactionListQuery, IEnumerable<ReactionListItemViewModel>>
{
    private readonly ReactionQueryDbContext _context;

    public ReactionQueryHandler(ReactionQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<ReactionListItemViewModel>> Handle(ReactionListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reactions.AsNoTracking()
            .Where(o => o.Issues.Any(e => e.IssueId == request.IssueId))
            .Select(o => new ReactionListItemViewModel
            {
                Id = o.ReactionId,
                Emoji = o.Emoji,
                Name = o.Name,
                Category = o.Category
            }).ToListAsync(cancellationToken);
    }
}