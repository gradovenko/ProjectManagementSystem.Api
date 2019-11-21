using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectIssueTimeEntries
{
    public sealed class TimeEntryQuery : IRequest<TimeEntryViewModel>
    {
        public Guid ProjectId { get; }
        public Guid IssueId { get; }
        public Guid TimeEntryId { get; }

        public TimeEntryQuery(Guid projectId, Guid issueId, Guid timeEntryId)
        {
            ProjectId = projectId;
            IssueId = issueId;
            TimeEntryId = timeEntryId;
        }
    }
}