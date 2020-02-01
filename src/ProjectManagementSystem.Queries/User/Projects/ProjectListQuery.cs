namespace ProjectManagementSystem.Queries.User.Projects
{
    public sealed class ProjectListQuery : PageQuery<ProjectListItemView>
    {
        public ProjectListQuery(int offset, int limit) : base(offset, limit) { }
    }
}