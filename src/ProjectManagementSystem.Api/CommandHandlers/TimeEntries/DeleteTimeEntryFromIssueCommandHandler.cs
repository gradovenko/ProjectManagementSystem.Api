using MediatR;
using ProjectManagementSystem.Domain.TimeEntries;
using ProjectManagementSystem.Domain.TimeEntries.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.TimeEntries;

public sealed class DeleteTimeEntryFromIssueCommandHandler : IRequestHandler<DeleteTimeEntryFromIssueCommand, DeleteTimeEntryFromIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IIssueGetter _issueGetter;
    private readonly ITimeEntryRepository _timeEntryRepository;

    public DeleteTimeEntryFromIssueCommandHandler(IUserGetter userGetter, IIssueGetter issueGetter,
        ITimeEntryRepository timeEntryRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _issueGetter = issueGetter ?? throw new ArgumentNullException(nameof(issueGetter));
        _timeEntryRepository = timeEntryRepository ?? throw new ArgumentNullException(nameof(timeEntryRepository));
    }

    public async Task<DeleteTimeEntryFromIssueCommandResultState> Handle(DeleteTimeEntryFromIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return DeleteTimeEntryFromIssueCommandResultState.UserNotFound;

        Issue? issue = await _issueGetter.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return DeleteTimeEntryFromIssueCommandResultState.IssueNotFound;
        
        TimeEntry? timeEntry = await _timeEntryRepository.Get(request.TimeEntryId, cancellationToken);
        
        if (timeEntry == null)
            return DeleteTimeEntryFromIssueCommandResultState.TimeEntryNotFound;

        timeEntry.Delete();

        await _timeEntryRepository.Save(timeEntry, cancellationToken);

        return DeleteTimeEntryFromIssueCommandResultState.TimeEntryFromEntryRemoved;
    }
}