namespace ProjectManagementSystem.Domain.Projects;

public sealed class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Path { get; private set; }
    public ProjectVisibility Visibility { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    private Guid _concurrencyToken;
    
    private Project() { }

    public Project(Guid id, string name, string? description, string path, ProjectVisibility visibility)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Visibility = visibility;
        CreateDate = UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string name, string? description, string path, ProjectVisibility visibility)
    {
        Name = name;
        Description = description;
        Path = path;
        Visibility = visibility;
        _concurrencyToken = Guid.NewGuid();
    }
    
    public void Delete()
    {
        IsDeleted = true;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}