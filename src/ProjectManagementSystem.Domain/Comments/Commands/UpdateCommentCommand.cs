using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record UpdateCommentCommand : IRequest<UpdateCommentCommandResultState>
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
}

public enum UpdateCommentCommandResultState
{
    CommentUpdated,
    CommentNotFound
}