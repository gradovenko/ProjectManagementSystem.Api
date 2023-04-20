using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record AddReactionToCommentCommand : IRequest<AddReactionToCommentCommandResultState>
{
    public Guid ReactionId { get; init; }
    public Guid CommentId { get; init; }
    public Guid UserId { get; init; }
}

public enum AddReactionToCommentCommandResultState
{
    CommentNotFound,
    UserNotFound,
    ReactionNotFound,
    CommentUserReactionAlreadyExists,
    ReactionToCommentAdded
}