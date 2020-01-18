namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public sealed class IssueStatusListQuery : PageQuery<IssueStatusListItemView>
    {
        public IssueStatusListQuery(int offset, int limit) : base(offset, limit) { }
    }
}