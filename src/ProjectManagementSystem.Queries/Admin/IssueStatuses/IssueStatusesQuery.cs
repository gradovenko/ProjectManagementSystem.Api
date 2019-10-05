namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public class IssueStatusesQuery : PageQuery<FullIssueStatusView>
    {
        public IssueStatusesQuery(int limit, int offset) : base(limit, offset) { }
    }
}