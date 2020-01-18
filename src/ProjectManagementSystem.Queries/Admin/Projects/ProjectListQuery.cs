namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public sealed class ProjectListQuery : PageQuery<ProjectListItemView>
    {
        public ProjectListQuery(int offset, int limit) : base(offset, limit) { }
    }
}