namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public class IssueStatusesQuery : PageQuery<FullIssueStatusView>
    {
        public IssueStatusesQuery(int offset, int limit) : base(offset, limit) { }
    }
}