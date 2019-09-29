namespace ProjectManagementSystem.Queries.Admin.Trackers
{
    public class TrackersQuery : PageQuery<FullTrackerView>
    {
        public TrackersQuery(int limit, int offset) : base(limit, offset) { }
    }
}