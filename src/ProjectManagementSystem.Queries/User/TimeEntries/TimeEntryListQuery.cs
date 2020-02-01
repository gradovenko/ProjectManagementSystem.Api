using System;

namespace ProjectManagementSystem.Queries.User.TimeEntries
{
    public sealed class TimeEntryListQuery : PageQuery<TimeEntryListItemView>
    {
        public TimeEntryListQuery(int offset, int limit) : base(offset, limit) { }
    }
}