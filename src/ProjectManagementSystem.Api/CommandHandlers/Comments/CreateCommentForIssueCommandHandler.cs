using MediatR;
using ProjectManagementSystem.Domain.Comments;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Comments;

public sealed class CreateCommentForIssueCommandHandler : IRequestHandler<CreateCommentForIssueCommand, CreateCommentForIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IIssueGetter _issueGetter;
    private readonly ICommentRepository _commentRepository;

    public CreateCommentForIssueCommandHandler(IUserGetter userGetter, IIssueGetter issueGetter,
        ICommentRepository commentRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _issueGetter = issueGetter ?? throw new ArgumentNullException(nameof(issueGetter));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<CreateCommentForIssueCommandResultState> Handle(CreateCommentForIssueCommand request,
        CancellationToken cancellationToken)
    {
        User? author = await _userGetter.Get(request.AuthorId, cancellationToken);

        if (author == null)
            return CreateCommentForIssueCommandResultState.AuthorNotFound;

        Issue? issue = await _issueGetter.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return CreateCommentForIssueCommandResultState.IssueNotFound;

        Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

        if (comment != null)
            return CreateCommentForIssueCommandResultState.CommentWithSameIdButOtherParamsAlreadyExists;

        if (request.ParentCommentId != null)
        {
            Comment? parentComment = await _commentRepository.Get(request.ParentCommentId.Value, cancellationToken);

            if (parentComment == null)
                return CreateCommentForIssueCommandResultState.ParentCommentNotFound;
            
            if (parentComment.ParentCommentId != null)
                return CreateCommentForIssueCommandResultState.ParentCommentAlreadyChildCommentOfAnotherComment;
        }

        await _commentRepository.Save(
            new Comment(request.CommentId, request.Content, request.AuthorId, request.IssueId, request.ParentCommentId),
            cancellationToken);

        return CreateCommentForIssueCommandResultState.CommentCreated;
    }
}