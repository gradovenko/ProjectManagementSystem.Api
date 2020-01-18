namespace ProjectManagementSystem.Queries.Admin.Users
{
    public sealed class UserListQuery : PageQuery<UserListItemView>
    {
        public UserListQuery(int offset, int limit) : base(offset, limit) { }
    }
}