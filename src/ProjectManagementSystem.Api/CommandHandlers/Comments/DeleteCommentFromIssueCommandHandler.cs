 using MediatR;
 using ProjectManagementSystem.Domain.Comments;
 using ProjectManagementSystem.Domain.Comments.Commands;

 namespace ProjectManagementSystem.Api.CommandHandlers.Comments;

 public sealed class DeleteCommentFromIssueCommandHandler : IRequestHandler<DeleteCommentFromIssueCommand, DeleteCommentFromIssueCommandResultState>
 {
     private readonly ICommentRepository _commentRepository;

     public DeleteCommentFromIssueCommandHandler(ICommentRepository commentRepository)
     {
         _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
     }
     
     public async Task<DeleteCommentFromIssueCommandResultState> Handle(DeleteCommentFromIssueCommand request, CancellationToken cancellationToken)
     {
         Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

         if (comment == null)
             return DeleteCommentFromIssueCommandResultState.CommentNotFound;

         comment.Delete();

         await _commentRepository.Save(comment, cancellationToken);

         return DeleteCommentFromIssueCommandResultState.CommentDeleted;
     }
 }