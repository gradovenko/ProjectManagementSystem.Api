using System;

namespace ProjectManagementSystem.Queries.User.IssueTimeEntries
{
    public sealed class TimeEntryListQuery : PageQuery<TimeEntryListView>
    {
        public Guid Id { get; }

        public TimeEntryListQuery(Guid id, int offset, int limit) : base(offset, limit)
        {
            Id = id;
        }
    }
}