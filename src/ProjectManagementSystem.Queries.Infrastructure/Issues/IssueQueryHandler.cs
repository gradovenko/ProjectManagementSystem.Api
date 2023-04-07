using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Issues;
using ProjectManagementSystem.Queries.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

public sealed class IssueQueryHandler : 
    IRequestHandler<IssueQuery, IssueViewModel?>,  
    IRequestHandler<IssueListQuery, PageViewModel<IssueListItemViewModel>>,
    IRequestHandler<ProjectIssueQuery, ProjectIssueViewModel?>,
    IRequestHandler<ProjectIssueListQuery, PageViewModel<ProjectIssueListItemViewModel>>
{
    private readonly IssueQueryDbContext _context;

    public IssueQueryHandler(IssueQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IssueViewModel?> Handle(IssueQuery query, CancellationToken cancellationToken)
    {
        return await _context.Issues.AsNoTracking()
            //.Include(i => i.Project)
            .Include(i => i.Author)
            .Include(i => i.Assignees)
            //.Include(i => i.ClosedByUser)
            .Where(i => i.IssueId == query.IssueId)
            .Select(i => new IssueViewModel
            {
                Id = i.IssueId,
                Title = i.Title,
                Description = i.Description,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate,
                DueDate = i.DueDate,
                Author = new AuthorViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                },
                Assignees = i.Assignees.Select(a => new AssigneeViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                })
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<PageViewModel<IssueListItemViewModel>> Handle(IssueListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Issues.AsNoTracking()
            .Include(i => i.Author)
            .Include(i => i.Assignees)
            .OrderBy(p => p.CreateDate)
            .Select(i => new IssueListItemViewModel
            {
                Id = i.IssueId,
                Title = i.Title,
                Description = i.Description,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate,
                DueDate = i.DueDate,
                Author = new AuthorViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                },
            })
            .AsQueryable();

        return new PageViewModel<IssueListItemViewModel>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }

    public async Task<ProjectIssueViewModel?> Handle(ProjectIssueQuery request, CancellationToken cancellationToken)
    {
        return await _context.Issues.AsNoTracking()
            //.Include(i => i.Project)
            .Include(i => i.Author)
            .Include(i => i.Assignees)
            //.Include(i => i.ClosedByUser)
            .Where(i => i.IssueId == request.IssueId)
            .Select(i => new ProjectIssueViewModel
            {
                Id = i.IssueId,
                Title = i.Title,
                Description = i.Description,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate,
                DueDate = i.DueDate,
                Author = new ProjectAuthorViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                },
                Assignees = i.Assignees.Select(a => new ProjectAssigneeViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                })
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PageViewModel<ProjectIssueListItemViewModel>> Handle(ProjectIssueListQuery request, CancellationToken cancellationToken)
    {
        var sql = _context.Issues.AsNoTracking()
            .Where(i => i.ProjectId == request.ProjectId)
            .Include(i => i.Author)
            .Include(i => i.Assignees)
            .OrderBy(p => p.CreateDate)
            .Select(i => new ProjectIssueListItemViewModel
            {
                Id = i.IssueId,
                Title = i.Title,
                Description = i.Description,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate,
                DueDate = i.DueDate,
                Author = new ProjectAuthorViewModel
                {
                    Id = i.Author.UserId,
                    Name = i.Author.Name
                },
            })
            .AsQueryable();

        return new PageViewModel<ProjectIssueListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}