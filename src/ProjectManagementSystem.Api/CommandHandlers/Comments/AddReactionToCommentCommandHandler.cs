// using MediatR;
// using ProjectManagementSystem.Domain.Comments;
// using ProjectManagementSystem.Domain.Comments.Commands;
//
// namespace ProjectManagementSystem.Api.CommandHandlers.Comments;
//
// public sealed class AddReactionToCommentCommandHandler : IRequestHandler<AddReactionToCommentCommand, AddReactionToCommentCommandResultState>
// {
//     private readonly ICommentRepository _commentRepository;
//     private readonly IReactionGetter _reactionGetter;
//
//     public AddReactionToCommentCommandHandler(ICommentRepository commentRepository, IReactionGetter reactionGetter)
//     {
//         _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
//         _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
//     }
//
//     public async Task<AddReactionToCommentCommandResultState> Handle(AddReactionToCommentCommand request, CancellationToken cancellationToken)
//     {
//         Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);
//
//         if (comment == null)
//             return AddReactionToCommentCommandResultState.CommentNotFound;
//
//         Reaction? reaction = await _reactionGetter.Get(request.Name, cancellationToken);
//
//         if (reaction == null)
//             return AddReactionToCommentCommandResultState.ReactionNotFound;
//
//         comment.AddReaction(reaction);
//
//         await _commentRepository.Save(comment, cancellationToken);
//
//         return AddReactionToCommentCommandResultState.ReactionToCommentAdded;
//     }
// }