namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public class ProjectsQuery : PageQuery<FullProjectView>
    {
        public ProjectsQuery(int offset, int limit) : base(offset, limit) { }
    }
}