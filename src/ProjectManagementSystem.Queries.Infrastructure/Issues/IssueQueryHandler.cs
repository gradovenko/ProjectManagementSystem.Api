using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Issues;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

public sealed class IssueQueryHandler : 
    IRequestHandler<IssueQuery, IssueViewModel?>,  
    IRequestHandler<IssueListQuery, Page<IssueListItemViewModel>>,
    IRequestHandler<ProjectIssueQuery, ProjectIssueViewModel?>,
    IRequestHandler<ProjectIssueListQuery, Page<ProjectIssueListItemViewModel>>
{
    private readonly IssueQueryDbContext _context;

    public IssueQueryHandler(IssueQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IssueViewModel?> Handle(IssueQuery query, CancellationToken cancellationToken)
    {
        return await _context.Issues.AsNoTracking()
            .Include(o => o.Project)
            .Include(o => o.Author)
            .Include(o => o.UserWhoClosed)
            .Where(o => o.IssueId == query.IssueId)
            .Select(o => new IssueViewModel
            {
                Id = o.IssueId,
                Title = o.Title,
                Description = o.Description,
                State = o.State,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                DueDate = o.DueDate,
                Project = new ProjectViewModel
                {
                    Id = o.Project.ProjectId,
                    Name = o.Project.Name,
                },
                Author = new AuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                },
                Assignees = o.Assignees.Select(a => new AssigneeViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                })
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<Page<IssueListItemViewModel>> Handle(IssueListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Issues.AsNoTracking()
            .Include(o => o.Author)
            .Include(o => o.Assignees)
            .OrderBy(p => p.CreateDate)
            .Select(o => new IssueListItemViewModel
            {
                Id = o.IssueId,
                Title = o.Title,
                Description = o.Description,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                DueDate = o.DueDate,
                Author = new AuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                },
            })
            .AsQueryable();

        return new Page<IssueListItemViewModel>
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
            //.Include(o => o.Project)
            .Include(o => o.Author)
            .Include(o => o.Assignees)
            //.Include(o => o.ClosedByUser)
            .Where(o => o.IssueId == request.IssueId)
            .Select(o => new ProjectIssueViewModel
            {
                Id = o.IssueId,
                Title = o.Title,
                Description = o.Description,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                DueDate = o.DueDate,
                Author = new ProjectAuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                },
                Assignees = o.Assignees.Select(a => new ProjectAssigneeViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                })
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Page<ProjectIssueListItemViewModel>> Handle(ProjectIssueListQuery request, CancellationToken cancellationToken)
    {
        var sql = _context.Issues.AsNoTracking()
            .Where(o => o.ProjectId == request.ProjectId)
            .Include(o => o.Author)
            .Include(o => o.Assignees)
            .OrderBy(p => p.CreateDate)
            .Select(o => new ProjectIssueListItemViewModel
            {
                Id = o.IssueId,
                Title = o.Title,
                Description = o.Description,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                DueDate = o.DueDate,
                Author = new ProjectAuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                },
            })
            .AsQueryable();

        return new Page<ProjectIssueListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}