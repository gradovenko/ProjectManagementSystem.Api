using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record AddReactionToCommentCommand : IRequest<AddReactionToCommentCommandResultState>
{
    public string Name { get; init; }
    public Guid CommentId { get; init; }
}

public enum AddReactionToCommentCommandResultState
{
    ReactionToCommentAdded,
    CommentNotFound,
    ReactionNotFound
}