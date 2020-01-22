using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.IssueTimeEntries
{
    public sealed class TimeEntryQuery : IRequest<TimeEntryView>
    {
        public Guid IssueId { get; }
        public Guid TimeEntryId { get; }

        public TimeEntryQuery(Guid issueId, Guid timeEntryId)
        {
            IssueId = issueId;
            TimeEntryId = timeEntryId;
        }
    }
}