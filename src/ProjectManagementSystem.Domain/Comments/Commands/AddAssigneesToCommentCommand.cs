using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record AddAssigneesToCommentCommand : IRequest<AddAssigneesToCommentCommandResultState>
{
    public string ReactionId { get; init; }
    public Guid CommentId { get; init; }
}

public enum AddAssigneesToCommentCommandResultState
{
    ReactionToCommentAdded,
    CommentNotFound,
    ReactionNotFound
}