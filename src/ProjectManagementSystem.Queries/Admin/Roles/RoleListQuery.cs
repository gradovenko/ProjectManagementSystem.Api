namespace ProjectManagementSystem.Queries.Admin.Roles
{
    public sealed class RoleListQuery : PageQuery<RoleListViewModel>
    {
        public RoleListQuery(int offset, int limit) : base(offset, limit) { }
    }
}