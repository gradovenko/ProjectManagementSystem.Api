namespace ProjectManagementSystem.Queries.Admin.Roles
{
    public sealed class RoleListQuery : PageQuery<RoleListItemView>
    {
        public RoleListQuery(int offset, int limit) : base(offset, limit) { }
    }
}