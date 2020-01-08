namespace ProjectManagementSystem.Queries.User.Projects
{
    public sealed class ProjectListQuery : PageQuery<ProjectListView>
    {
        public ProjectListQuery(int offset, int limit) : base(offset, limit) { }
    }
}