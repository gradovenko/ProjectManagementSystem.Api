namespace ProjectManagementSystem.Queries.User.Issues
{
    public sealed class IssueListQuery : PageQuery<IssueListItemView>
    {
        public IssueListQuery(int offset, int limit) : base(offset, limit) { }
    }
}