namespace ProjectManagementSystem.Queries.User.Projects
{
    public sealed class ProjectsQuery : PageQuery<ProjectsView>
    {
        public ProjectsQuery(int limit, int offset) : base(limit, offset) { }
    }
}