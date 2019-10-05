using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Projects;
using ProjectManagementSystem.Queries.Infrastructure.Extensions;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects
{
    public class ProjectsQueryHandler : IRequestHandler<ProjectsQuery, Page<FullProjectView>>
    {
        private readonly ProjectDbContext _context;

        public ProjectsQueryHandler(ProjectDbContext context)
        {
            _context = context;
        }
        
        public async Task<Page<FullProjectView>> Handle(ProjectsQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Projects.AsNoTracking()
                .Select(project => new FullProjectView
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    IsPrivate = project.IsPrivate,
                    CreateDate = project.CreateDate
                });

            return new Page<FullProjectView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}