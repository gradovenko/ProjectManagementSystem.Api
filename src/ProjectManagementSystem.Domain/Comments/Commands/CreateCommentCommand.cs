using MediatR;

namespace ProjectManagementSystem.Domain.Comments.Commands;

public sealed record CreateCommentCommand : IRequest<CreateCommentCommandResultState>
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
}

public enum CreateCommentCommandResultState
{
    CommentCreated,
    CommentWithSameIdButOtherParamsAlreadyExists
}