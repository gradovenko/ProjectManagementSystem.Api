namespace ProjectManagementSystem.Queries.Admin.Users
{
    public class UsersQuery : PageQuery<FullUserView>
    {
        public UsersQuery(int offset, int limit) : base(offset, limit) { }
    }
}