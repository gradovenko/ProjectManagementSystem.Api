using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, CreateIssueCommandResultState>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectGetter _projectGetter;
    private readonly IUserGetter _userGetter;
    private readonly ILabelGetter _labelGetter;
    private readonly IIssueAssigneeGetter _issueAssigneeRepository;
    private readonly IIssueLabelGetter _issueLabelRepository;

    public CreateIssueCommandHandler(IIssueRepository issueRepository, IProjectGetter projectGetter,
        IUserGetter userGetter, ILabelGetter labelGetter, IIssueAssigneeGetter issueAssigneeRepository,
        IIssueLabelGetter issueLabelRepository)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _projectGetter = projectGetter ?? throw new ArgumentNullException(nameof(projectGetter));
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _labelGetter = labelGetter ?? throw new ArgumentNullException(nameof(labelGetter));
        _issueAssigneeRepository =
            issueAssigneeRepository ?? throw new ArgumentNullException(nameof(issueAssigneeRepository));
        _issueLabelRepository = issueLabelRepository ?? throw new ArgumentNullException(nameof(issueLabelRepository));
    }

    public async Task<CreateIssueCommandResultState> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectGetter.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return CreateIssueCommandResultState.ProjectNotFound;
         
        User? author = await _userGetter.Get(request.AuthorId, cancellationToken);

        if (author == null)
            return CreateIssueCommandResultState.AuthorNotFound;

        var issueAssignees = new List<IssueAssignee>();

        if (request.AssigneeIds != null && request.AssigneeIds.Any())
        {
            foreach (Guid assigneeId in request.AssigneeIds)
            {
                User? assignee = await _userGetter.Get(assigneeId, cancellationToken);

                if (assignee == null)
                    return CreateIssueCommandResultState.AssigneeNotFound;

                IssueAssignee? issueAssignee =
                    await _issueAssigneeRepository.Get(request.IssueId, assignee.Id, cancellationToken);
                
                if (issueAssignee != null)
                    return CreateIssueCommandResultState.IssueAssigneeAlreadyExists;

                issueAssignees.Add(new IssueAssignee(request.IssueId, assigneeId));
            }
        }
        
        var issueLabels = new List<IssueLabel>();

        if (request.LabelIds != null && request.LabelIds.Any())
        {
            foreach (Guid labelId in request.LabelIds)
            {
                Label? label = await _labelGetter.Get(labelId, cancellationToken);

                if (label == null)
                    return CreateIssueCommandResultState.LabelNotFound;

                IssueLabel? issueLabel =
                    await _issueLabelRepository.Get(request.IssueId, label.Id, cancellationToken);
                
                if (issueLabel != null)
                    return CreateIssueCommandResultState.IssueAssigneeAlreadyExists;

                issueLabels.Add(new IssueLabel(request.IssueId, labelId));
            }
        }

        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue != null)
            return CreateIssueCommandResultState.IssueAlreadyExists;

        issue = new Issue(request.IssueId, request.Title, request.Description, request.DueDate, request.ProjectId,
            request.AuthorId, issueAssignees, issueLabels);

        await _issueRepository.Save(issue, cancellationToken);

        return CreateIssueCommandResultState.IssueCreated;
    }
}