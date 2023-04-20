using MediatR;
using ProjectManagementSystem.Domain.Reactions;
using ProjectManagementSystem.Domain.Reactions.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Reactions;

public class CreateReactionCommandHandler : IRequestHandler<CreateReactionCommand, CreateReactionCommandResultState>
{
    private readonly IReactionRepository _reactionRepository;

    public CreateReactionCommandHandler(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
    }

    public async Task<CreateReactionCommandResultState> Handle(CreateReactionCommand request,
        CancellationToken cancellationToken)
    {
        Reaction? reaction = await _reactionRepository.Get(request.ReactionId, cancellationToken);

        if (reaction != null)
        {
            if (reaction.Emoji == request.Emoji)
                return CreateReactionCommandResultState.ReactionWithSameEmojiAlreadyExists;
            if (reaction.Name == request.Name)
                return CreateReactionCommandResultState.ReactionWithSameNameAlreadyExists;
            return CreateReactionCommandResultState.ReactionWithSameIdButOtherParamsAlreadyExists;
        }

        await _reactionRepository.Save(
            new Reaction(request.ReactionId, request.Emoji, request.Name, request.Category), cancellationToken);

        return CreateReactionCommandResultState.ReactionCreated;
    }
}