using MediatR;
using ProjectManagementSystem.Domain.TimeEntries;
using ProjectManagementSystem.Domain.TimeEntries.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.TimeEntries;

public sealed class CreateTimeEntryForIssueCommandHandler : IRequestHandler<CreateTimeEntryForIssueCommand, CreateTimeEntryForIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IIssueGetter _issueGetter;
    private readonly ITimeEntryRepository _timeEntryRepository;

    public CreateTimeEntryForIssueCommandHandler(IUserGetter userGetter, IIssueGetter issueGetter,
        ITimeEntryRepository timeEntryRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _issueGetter = issueGetter ?? throw new ArgumentNullException(nameof(issueGetter));
        _timeEntryRepository = timeEntryRepository ?? throw new ArgumentNullException(nameof(timeEntryRepository));
    }

    public async Task<CreateTimeEntryForIssueCommandResultState> Handle(CreateTimeEntryForIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return CreateTimeEntryForIssueCommandResultState.UserNotFound;

        Issue? issue = await _issueGetter.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return CreateTimeEntryForIssueCommandResultState.IssueNotFound;
        
        TimeEntry? timeEntry = await _timeEntryRepository.Get(request.TimeEntryId, cancellationToken);

        if (timeEntry != null)
            return CreateTimeEntryForIssueCommandResultState.TimeEntryWithSameIdAlreadyExists;

        await _timeEntryRepository.Save(
            new TimeEntry(request.TimeEntryId, request.Hours, request.Description, request.DueDate, request.IssueId, request.UserId), 
            cancellationToken);

        return CreateTimeEntryForIssueCommandResultState.TimeEntryForIssueCreated;
    }
}