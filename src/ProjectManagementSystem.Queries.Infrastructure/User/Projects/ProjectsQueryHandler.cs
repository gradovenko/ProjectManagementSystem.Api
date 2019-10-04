using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Projects;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Projects
{
    public sealed class ProjectsQueryHandler : IRequestHandler<ProjectsQuery, Page<ProjectsView>>
    {
        private readonly ProjectDbContext _context;

        public ProjectsQueryHandler(ProjectDbContext context)
        {
            _context = context;
        }
        
        public async Task<Page<ProjectsView>> Handle(ProjectsQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Projects.AsNoTracking()
                .Select(project => new ProjectsView
                {
                    Name = project.Name,
                    Description = project.Description,
                    IsPrivate = project.IsPrivate,
                });

            return new Page<ProjectsView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}