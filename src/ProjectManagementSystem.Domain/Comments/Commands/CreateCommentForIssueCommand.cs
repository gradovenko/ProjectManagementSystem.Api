using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record CreateCommentForIssueCommand : IRequest<CreateCommentForIssueCommandResultState>
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
    public Guid AuthorId { get; init; }
    public Guid IssueId { get; init; }
    public Guid? ParentCommentId { get; init; }
}

public enum CreateCommentForIssueCommandResultState
{
    AuthorNotFound,
    IssueNotFound,
    CommentWithSameIdButOtherParamsAlreadyExists,
    ParentCommentNotFound,
    CommentCreated
}