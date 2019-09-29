namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public class ProjectsQuery : PageQuery<FullProjectView>
    {
        public ProjectsQuery(int limit, int offset) : base(limit, offset) { }
    }
}