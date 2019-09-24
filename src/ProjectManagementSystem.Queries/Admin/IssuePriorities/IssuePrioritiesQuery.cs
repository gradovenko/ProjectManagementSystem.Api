namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePrioritiesQuery : PageQuery<IssuePriorityView>
    {
        public IssuePrioritiesQuery(int offset, int limit) : base(offset, limit) { }
    }
}