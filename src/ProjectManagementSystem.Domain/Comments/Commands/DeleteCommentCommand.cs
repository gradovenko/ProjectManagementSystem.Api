using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record DeleteCommentCommand : IRequest<DeleteCommentCommandResultState>
{
    public Guid CommentId { get; init; }
}

public enum DeleteCommentCommandResultState
{
    CommentDeleted,
    CommentNotFound
}