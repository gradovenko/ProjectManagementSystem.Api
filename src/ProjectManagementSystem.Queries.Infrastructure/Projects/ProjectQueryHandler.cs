using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Projects;

namespace ProjectManagementSystem.Queries.Infrastructure.Projects;

public sealed class ProjectQueryHandler : 
    IRequestHandler<ProjectListQuery, PageViewModel<ProjectListItemViewModel>>,
    IRequestHandler<ProjectQuery, ProjectViewModel?>
{
    private readonly ProjectQueryDbContext _context;

    public ProjectQueryHandler(ProjectQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<PageViewModel<ProjectListItemViewModel>> Handle(ProjectListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Projects.AsNoTracking()
            .Select(p => new ProjectListItemViewModel
            {
                Id = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Path = p.Path,
                Visibility = p.Visibility,
                IsDeleted = p.IsDeleted,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate
            });

        return new PageViewModel<ProjectListItemViewModel>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }

    public async Task<ProjectViewModel?> Handle(ProjectQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects.AsNoTracking()
            .Where(p => p.ProjectId == request.Id)
            .Select(p => new ProjectViewModel
            {
                Id = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Path = p.Path,
                Visibility = p.Visibility,
                IsDeleted = p.IsDeleted,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}