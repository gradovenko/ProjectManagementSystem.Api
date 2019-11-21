using System;

namespace ProjectManagementSystem.Queries.User.ProjectTimeEntries
{
    public sealed class TimeEntryListQuery : PageQuery<TimeEntryListViewModel>
    {
        public Guid ProjectId { get; set; }

        public TimeEntryListQuery(Guid projectId, int offset, int limit) : base(offset, limit)
        {
            ProjectId = projectId;
        }
    }
}