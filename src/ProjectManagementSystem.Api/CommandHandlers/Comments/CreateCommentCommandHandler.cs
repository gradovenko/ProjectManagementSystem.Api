using MediatR;
using ProjectManagementSystem.Domain.Comments;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Comments;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandResultState>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<CreateCommentCommandResultState> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

        if (comment != null)
            return CreateCommentCommandResultState.CommentWithSameIdButOtherParamsAlreadyExists;

        await _commentRepository.Save(new Comment(request.CommentId, request.Content), cancellationToken);

        return CreateCommentCommandResultState.CommentCreated;
    }
}