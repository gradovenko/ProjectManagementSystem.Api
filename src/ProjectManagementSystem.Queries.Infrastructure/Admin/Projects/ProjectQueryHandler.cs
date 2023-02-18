using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Projects;
using ProjectManagementSystem.Queries.Infrastructure.Extensions;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects;

public sealed class ProjectQueryHandler : IRequestHandler<ProjectQuery, ProjectView>
{
    private readonly ProjectDbContext _context;

    public ProjectQueryHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectView> Handle(ProjectQuery query, CancellationToken cancellationToken)
    {
        return await _context.Projects.AsNoTracking()
            .Where(project => project.Id == query.Id)
            .Select(project => new ProjectView
            {
                Name = project.Name,
                Description = project.Description,
                IsPrivate = project.IsPrivate,
                Status = project.Status.ConvertTo<Queries.Admin.Projects.ProjectStatus>(),
                CreateDate = project.CreateDate
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}