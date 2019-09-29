namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePrioritiesQuery : PageQuery<FullIssuePriorityView>
    {
        public IssuePrioritiesQuery(int limit, int offset) : base(limit, offset) { }
    }
}