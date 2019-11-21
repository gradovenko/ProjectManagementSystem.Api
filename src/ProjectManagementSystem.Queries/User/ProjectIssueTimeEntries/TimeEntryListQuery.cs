using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssueTimeEntries
{
    public class TimeEntryListQuery : PageQuery<TimeEntryListViewModel>
    {
        public Guid ProjectId { get; set; }
        public Guid IssueId { get; set; }

        public TimeEntryListQuery(Guid projectId, Guid issueId, int offset, int limit) : base(offset, limit)
        {
            ProjectId = projectId;
            IssueId = issueId;
        }
    }
}