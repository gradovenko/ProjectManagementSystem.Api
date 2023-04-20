using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.IssueComments;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueComments;

public sealed class CommentQueryHandler : IRequestHandler<CommentListQuery, Page<CommentListItemViewModel>> 
{
    private readonly CommentQueryDbContext _context;

    public CommentQueryHandler(CommentQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Page<CommentListItemViewModel>> Handle(CommentListQuery request, CancellationToken cancellationToken)
    {
        var sql = _context.Comments.AsNoTracking()
            .Include(o => o.Author)
            .Where(o => o.IssueId == request.IssueId)
            .Where(o => o.ParentCommentId == null)
            .Select(o => new CommentListItemViewModel
            {
                Id = o.CommentId,
                Content = o.Content,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                Author = new AuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                },
                ParentCommentId = o.ParentCommentId,
                ChildComments = o.ChildComments.Select(oo => new CommentListItemViewModel
                {
                    Id = o.CommentId,
                    Content = o.Content,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    Author = new AuthorViewModel
                    {
                        Id = o.Author.UserId,
                        Name = o.Author.Name
                    },
                    ParentCommentId = o.ParentCommentId
                })
            });

        return new Page<CommentListItemViewModel>
        {
            Limit = request.Limit,
            Offset = request.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken)
        };
    }
}