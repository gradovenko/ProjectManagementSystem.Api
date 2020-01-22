namespace ProjectManagementSystem.Queries.Admin.Trackers
{
    public sealed class TrackerListQuery : PageQuery<TrackerListItemView>
    {
        public TrackerListQuery(int offset, int limit) : base(offset, limit) { }
    }
}