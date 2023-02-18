namespace ProjectManagementSystem.Domain.Admin.Projects;

public sealed class Project
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsPrivate { get; }
    public ProjectStatus Status { get; }
    public DateTime CreateDate { get; }
    private List<ProjectTracker> _projectTrackers = new List<ProjectTracker>();
    public IEnumerable<ProjectTracker> ProjectTrackers => _projectTrackers;
    private Guid _concurrencyStamp;

    public Project(Guid id, string name, string description, bool isPrivate)
    {
        Id = id;
        Name = name;
        Description = description;
        IsPrivate = isPrivate;
        Status = ProjectStatus.Active;
        CreateDate = DateTime.UtcNow;
        _concurrencyStamp = Guid.NewGuid();
    }
        
    public void AddProjectTracker(ProjectTracker projectTracker)
    {
        _projectTrackers.Add(projectTracker);
        _concurrencyStamp = Guid.NewGuid();
    }
}