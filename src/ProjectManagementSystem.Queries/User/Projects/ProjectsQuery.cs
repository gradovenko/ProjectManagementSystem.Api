namespace ProjectManagementSystem.Queries.User.Projects
{
    public sealed class ProjectsQuery : PageQuery<ProjectsView>
    {
        public ProjectsQuery(int offset, int limit) : base(offset, limit) { }
    }
}