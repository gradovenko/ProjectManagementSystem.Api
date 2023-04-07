using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record CreateCommentForPostCommand : IRequest<CreateCommentForPostCommandResultState>
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
}

public enum CreateCommentForPostCommandResultState
{
    CommentCreated,
    CommentWithSameIdButOtherParamsAlreadyExists
}