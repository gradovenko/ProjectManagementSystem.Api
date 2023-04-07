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

    public CreateIssueCommandHandler(IIssueRepository issueRepository, IProjectGetter projectGetter,
        IUserGetter userGetter, ILabelGetter labelGetter)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _projectGetter = projectGetter ?? throw new ArgumentNullException(nameof(projectGetter));
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _labelGetter = labelGetter ?? throw new ArgumentNullException(nameof(labelGetter));
    }

    public async Task<CreateIssueCommandResultState> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectGetter.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return CreateIssueCommandResultState.ProjectNotFound;
        
        User? author = await _userGetter.Get(request.ProjectId, cancellationToken);

        if (author == null)
            return CreateIssueCommandResultState.AuthorNotFound;

        var assignees = new List<User>();

        if (request.AssigneeIds.Any())
        {
            foreach (Guid assigneeId in request.AssigneeIds)
            {
                User? assignee = await _userGetter.Get(assigneeId, cancellationToken);

                if (assignee == null)
                    return CreateIssueCommandResultState.AssigneeNotFound;

                assignees.Add(assignee);
            }
        }
        
        var labels = new List<Label>();

        if (request.LabelIds.Any())
        {
            foreach (Guid labelId in request.LabelIds)
            {
                Label? label = await _labelGetter.Get(labelId, cancellationToken);

                if (label == null)
                    return CreateIssueCommandResultState.LabelNotFound;

                labels.Add(label);
            }
        }

        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue != null)
            return CreateIssueCommandResultState.IssueAlreadyExists;

        issue = new Issue(request.IssueId, request.Title, request.Description, request.DueDate, request.ProjectId,
            request.AuthorId, assignees, labels);

        await _issueRepository.Save(issue, cancellationToken);

        return CreateIssueCommandResultState.IssueCreated;
    }
}