// using MediatR;
// using ProjectManagementSystem.Domain.Comments;
// using ProjectManagementSystem.Domain.Comments.Commands;
//
// namespace ProjectManagementSystem.Api.CommandHandlers.Comments;
//
// public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentForPostCommand, CreateCommentForPostCommandResultState>
// {
//     private readonly ICommentRepository _commentRepository;
//
//     public CreateCommentCommandHandler(ICommentRepository commentRepository)
//     {
//         _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
//     }
//
//     public async Task<CreateCommentForPostCommandResultState> Handle(CreateCommentForPostCommand request, CancellationToken cancellationToken)
//     {
//         Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);
//
//         if (comment != null)
//             return CreateCommentForPostCommandResultState.CommentWithSameIdButOtherParamsAlreadyExists;
//
//         await _commentRepository.Save(new Comment(request.CommentId, request.Content), cancellationToken);
//
//         return CreateCommentForPostCommandResultState.CommentCreated;
//     }
// }