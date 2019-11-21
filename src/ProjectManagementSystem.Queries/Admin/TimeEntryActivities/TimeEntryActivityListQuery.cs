namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities
{
    public class TimeEntryActivityListQuery : PageQuery<TimeEntryActivityListViewModel>
    {
        public TimeEntryActivityListQuery(int offset, int limit) : base(offset, limit) { }
    }
}