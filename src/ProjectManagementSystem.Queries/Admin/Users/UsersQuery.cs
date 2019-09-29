namespace ProjectManagementSystem.Queries.Admin.Users
{
    public class UsersQuery : PageQuery<FullUserView>
    {
        public UsersQuery(int limit, int offset) : base(limit, offset) { }
    }
}