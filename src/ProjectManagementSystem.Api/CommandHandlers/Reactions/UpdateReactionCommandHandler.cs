using MediatR;
using ProjectManagementSystem.Domain.Reactions;
using ProjectManagementSystem.Domain.Reactions.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Reactions;

public sealed class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, UpdateReactionCommandResultState>
{
    private readonly IReactionRepository _reactionRepository;

    public UpdateReactionCommandHandler(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
    }

    public async Task<UpdateReactionCommandResultState> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
    {
        Reaction? reaction = await _reactionRepository.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return UpdateReactionCommandResultState.ReactionNotFound;

        if (request.Emoji != reaction.Emoji)
        {
            Reaction? reactionWithSameEmoji = await _reactionRepository.GetByEmoji(request.Emoji, cancellationToken);

            if (reactionWithSameEmoji != null)
                return UpdateReactionCommandResultState.ReactionWithSameEmojiAlreadyExists;
        }

        if (request.Name != reaction.Name)
        {
            Reaction? reactionWithSameName = await _reactionRepository.GetByName(request.Emoji, cancellationToken);

            if (reactionWithSameName != null)
                return UpdateReactionCommandResultState.ReactionWithSameNameAlreadyExists;
        }

        reaction.Update(request.Emoji, request.Name, request.Category);

        await _reactionRepository.Save(reaction, cancellationToken);

        return UpdateReactionCommandResultState.ReactionUpdated;
    }
}