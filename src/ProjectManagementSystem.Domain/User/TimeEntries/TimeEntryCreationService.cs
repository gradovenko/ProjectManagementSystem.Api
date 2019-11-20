using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public sealed class TimeEntryCreationService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITimeEntryActivityRepository _timeEntryActivityRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryCreationService(IProjectRepository projectRepository, IIssueRepository issueRepository,
            IUserRepository userRepository, ITimeEntryActivityRepository timeEntryActivityRepository,
            ITimeEntryRepository timeEntryRepository)
        {
            _projectRepository = projectRepository;
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _timeEntryActivityRepository = timeEntryActivityRepository;
            _timeEntryRepository = timeEntryRepository;
        }

        public async Task CreateTimeEntry(Guid projectId, Guid issueId, Guid timeEntryId, decimal hours,
            string description, DateTime dueDate, Guid userId, Guid activityId, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ProjectNotFoundException();

            var issue = await _issueRepository.Get(issueId, cancellationToken);

            if (issue == null)
                throw new IssueNotFoundException();

            var user = await _userRepository.Get(userId, cancellationToken);

            if (user == null)
                throw new UserNotFoundException();
            
            var activity = await _timeEntryActivityRepository.Get(activityId, cancellationToken);

            if (activity == null)
                throw new TimeEntryActivityNotFoundException();
            
            var timeEntry = await _timeEntryRepository.Get(issueId, cancellationToken);

            if (timeEntry != null)
                throw new TimeEntryAlreadyExistsException();

            timeEntry = new TimeEntry(timeEntryId, hours, description, dueDate, projectId, userId, activityId);

            issue.AddTimeEntry(timeEntry);

            await _issueRepository.Save(issue);
        }
    }
}