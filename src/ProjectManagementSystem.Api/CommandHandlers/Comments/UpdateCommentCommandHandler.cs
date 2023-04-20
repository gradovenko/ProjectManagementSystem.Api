using MediatR;
using ProjectManagementSystem.Domain.Comments;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Comments;

public sealed class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentForIssueCommand, UpdateCommentForIssueCommandResultState>
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<UpdateCommentForIssueCommandResultState> Handle(UpdateCommentForIssueCommand request, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

        if (comment == null)
            return UpdateCommentForIssueCommandResultState.CommentNotFound;

        comment.Update(request.Content);

        await _commentRepository.Save(comment, cancellationToken);

        return UpdateCommentForIssueCommandResultState.CommentUpdated;
    }
}