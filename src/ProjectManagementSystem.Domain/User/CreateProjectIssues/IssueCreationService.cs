using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public sealed class IssueCreationService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITrackerRepository _trackerRepository;
        private readonly IIssueStatusRepository _issueStatusRepository;
        private readonly IIssuePriorityRepository _issuePriorityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIssueRepository _issueRepository;

        public IssueCreationService(IProjectRepository projectRepository, ITrackerRepository trackerRepository,
            IIssueStatusRepository issueStatusRepository, IIssuePriorityRepository issuePriorityRepository,
            IUserRepository userRepository, IIssueRepository issueRepository)
        {
            _projectRepository = projectRepository;
            _trackerRepository = trackerRepository;
            _issueStatusRepository = issueStatusRepository;
            _issuePriorityRepository = issuePriorityRepository;
            _userRepository = userRepository;
            _issueRepository = issueRepository;
        }

        public async Task CreateIssue(Guid projectId, Guid issueId, string title, string description, DateTime? startDate, DateTime? endDate,
            Guid trackerId, Guid statusId, Guid priorityId, Guid authorId, Guid? performerId,
            CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new ProjectNotFoundException();

            var tracker = await _trackerRepository.Get(trackerId, cancellationToken);

            if (tracker == null)
                throw new TrackerNotFoundException();
            
            var issueStatus = await _issueStatusRepository.Get(statusId, cancellationToken);

            if (issueStatus == null)
                throw new IssueStatusNotFoundException();
            
            var issuePriority = await _issuePriorityRepository.Get(priorityId, cancellationToken);

            if (issuePriority == null)
                throw new IssuePriorityNotFoundException();

            if (performerId != null)
            {
                var performer = await _userRepository.Get(performerId.Value, cancellationToken);

                if (performer == null)
                    throw new PerformerNotFoundException();
            }

            var issue = await _issueRepository.Get(issueId, cancellationToken);

            if (issue != null)
                throw new IssueAlreadyExistsException();

            issue = new Issue(issueId, title, description, startDate, endDate, trackerId, statusId, priorityId, authorId, performerId);

            project.AddIssue(issue);

            await _projectRepository.Save(project);
        }
    }
}