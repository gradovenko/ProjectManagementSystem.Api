using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record DeleteCommentFromIssueCommand : IRequest<DeleteCommentFromIssueCommandResultState>
{
    public Guid CommentId { get; init; }
}

public enum DeleteCommentFromIssueCommandResultState
{
    CommentNotFound,
    CommentDeleted
}