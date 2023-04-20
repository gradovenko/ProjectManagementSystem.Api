using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.IssueLabels;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueLabels;

public sealed class LabelQueryHandler : IRequestHandler<LabelListQuery, IEnumerable<LabelListItemViewModel>>
{
    private readonly LabelQueryDbContext _context;

    public LabelQueryHandler(LabelQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<LabelListItemViewModel>> Handle(LabelListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Labels.AsNoTracking()
            .Where(o => o.Issues.Any(e => e.IssueId == request.IssueId))
            .OrderBy(p => p.CreateDate)
            .Select(i => new LabelListItemViewModel
            {
                Id = i.LabelId,
                Title = i.Title,
                Description = i.Description,
                BackgroundColor = i.BackgroundColor,
                IsDeleted = i.IsDeleted,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate
            })
            .ToListAsync(cancellationToken);
    }
}