using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record UpdateCommentForIssueCommand : IRequest<UpdateCommentForIssueCommandResultState>
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
}

public enum UpdateCommentForIssueCommandResultState
{
    CommentNotFound,
    CommentUpdated
}