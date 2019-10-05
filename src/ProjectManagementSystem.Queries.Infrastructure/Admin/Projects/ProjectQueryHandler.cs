using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Projects;
using ProjectManagementSystem.Queries.Infrastructure.Extensions;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects
{
    public sealed class ProjectQueryHandler : IRequestHandler<ProjectQuery, ShortProjectView>
    {
        private readonly ProjectDbContext _context;

        public ProjectQueryHandler(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<ShortProjectView> Handle(ProjectQuery query, CancellationToken cancellationToken)
        {
            return await _context.Projects.AsNoTracking()
                .Where(project => project.Id == query.Id)
                .Select(project => new ShortProjectView
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
}