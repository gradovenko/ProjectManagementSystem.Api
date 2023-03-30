using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Projects;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects;

public sealed class ProjectListQueryHandler : IRequestHandler<ProjectListQuery, PageViewModel<ProjectListItemView>>
{
    private readonly ProjectDbContext _context;

    public ProjectListQueryHandler(ProjectDbContext context)
    {
        _context = context;
    }
        
    public async Task<PageViewModel<ProjectListItemView>> Handle(ProjectListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Projects.AsNoTracking()
            .Select(project => new ProjectListItemView
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                IsPrivate = project.IsPrivate,
                CreateDate = project.CreateDate
            });

        return new PageViewModel<ProjectListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}