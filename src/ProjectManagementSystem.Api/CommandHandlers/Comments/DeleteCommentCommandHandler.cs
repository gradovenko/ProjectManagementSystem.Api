// using MediatR;
// using ProjectManagementSystem.Domain.Comments;
// using ProjectManagementSystem.Domain.Comments.Commands;
//
// namespace ProjectManagementSystem.Api.CommandHandlers.Comments;
//
// public sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, DeleteCommentCommandResultState>
// {
//     private readonly ICommentRepository _commentRepository;
//
//     public DeleteCommentCommandHandler(ICommentRepository commentRepository)
//     {
//         _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
//     }
//     
//     public async Task<DeleteCommentCommandResultState> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
//     {
//         Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);
//
//         if (comment == null)
//             return DeleteCommentCommandResultState.CommentNotFound;
//
//         comment.Delete();
//
//         await _commentRepository.Save(comment, cancellationToken);
//
//         return DeleteCommentCommandResultState.CommentDeleted;
//     }
// }