namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueListQuery : PageQuery<IssueListView>
    {
        public IssueListQuery(int limit, int offset) : base(limit, offset) { }
    }
}