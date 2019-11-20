namespace ProjectManagementSystem.Queries.Admin.Trackers
{
    public class TrackersQuery : PageQuery<FullTrackerView>
    {
        public TrackersQuery(int offset, int limit) : base(offset, limit) { }
    }
}