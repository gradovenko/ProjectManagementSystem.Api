using System;

namespace ProjectManagementSystem.Queries.User.ProjectTimeEntries
{
    public sealed class TimeEntryListQuery : PageQuery<TimeEntryListView>
    {
        public Guid ProjectId { get; }

        public TimeEntryListQuery(Guid projectId, int offset, int limit) : base(offset, limit)
        {
            ProjectId = projectId;
        }
    }
}