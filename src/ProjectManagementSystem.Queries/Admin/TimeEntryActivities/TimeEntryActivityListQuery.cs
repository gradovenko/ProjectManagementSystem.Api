namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivityListQuery : PageQuery<TimeEntryActivityListItemView>
    {
        public TimeEntryActivityListQuery(int offset, int limit) : base(offset, limit) { }
    }
}