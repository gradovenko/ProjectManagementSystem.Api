namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePrioritiesQuery : PageQuery<FullIssuePriorityView>
    {
        public IssuePrioritiesQuery(int offset, int limit) : base(offset, limit) { }
    }
}