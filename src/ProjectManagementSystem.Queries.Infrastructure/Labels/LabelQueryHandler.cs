using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Labels;

namespace ProjectManagementSystem.Queries.Infrastructure.Labels;

public sealed class LabelQueryHandler : IRequestHandler<LabelListQuery, PageViewModel<LabelListItemViewModel>>
{
    private readonly LabelQueryDbContext _context;

    public LabelQueryHandler(LabelQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<PageViewModel<LabelListItemViewModel>> Handle(LabelListQuery request, CancellationToken cancellationToken)
    {
        var sql = _context.Labels.AsNoTracking()
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
            .AsQueryable();

        return new PageViewModel<LabelListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}