using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Projects;

namespace ProjectManagementSystem.Queries.Infrastructure.Projects;

public sealed class ProjectQueryHandler : 
    IRequestHandler<ProjectListQuery, Page<ProjectListItemViewModel>>,
    IRequestHandler<ProjectQuery, ProjectViewModel?>
{
    private readonly ProjectQueryDbContext _context;

    public ProjectQueryHandler(ProjectQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Page<ProjectListItemViewModel>> Handle(ProjectListQuery request, CancellationToken cancellationToken)
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

        return new Page<ProjectListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
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