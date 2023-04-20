using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record RemoveReactionFromCommentCommand : IRequest<RemoveReactionFromCommentCommandResultState>
{
    public Guid ReactionId { get; init; }
    public Guid CommentId { get; init; }
    public Guid UserId { get; init; }
}

public enum RemoveReactionFromCommentCommandResultState
{
    CommentNotFound,
    UserNotFound,
    ReactionNotFound,
    CommentUserReactionNotFound,
    ReactionFromCommentRemoved
}