namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public sealed class IssuePriorityListQuery : PageQuery<IssuePriorityListItemView>
    {
        public IssuePriorityListQuery(int offset, int limit) : base(offset, limit) { }
    }
}