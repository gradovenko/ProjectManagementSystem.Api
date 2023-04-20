using MediatR;

namespace ProjectManagementSystem.Domain.Reactions.Commands;

public sealed record DeleteReactionCommand : IRequest<DeleteReactionCommandResultState>
{
    public Guid ReactionId { get; init; }
}

public enum DeleteReactionCommandResultState
{
    ReactionNotFound,
    ReactionDeleted
}