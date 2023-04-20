using MediatR;
using ProjectManagementSystem.Domain.Reactions;
using ProjectManagementSystem.Domain.Reactions.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Reactions;

public sealed class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, DeleteReactionCommandResultState>
{
    private readonly IReactionRepository _reactionRepository;

    public DeleteReactionCommandHandler(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
    }

    public async Task<DeleteReactionCommandResultState> Handle(DeleteReactionCommand request,
        CancellationToken cancellationToken)
    {
        Reaction? reaction = await _reactionRepository.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return DeleteReactionCommandResultState.ReactionNotFound;

        reaction.Delete();

        await _reactionRepository.Save(reaction, cancellationToken);

        return DeleteReactionCommandResultState.ReactionDeleted;
    }
}