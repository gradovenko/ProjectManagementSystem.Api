namespace ProjectManagementSystem.Domain.Labels;

public sealed class Label
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string BackgroundColor { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    private Guid _concurrencyToken;

    private Label() { }

    public Label(Guid id, string title, string? description, string backgroundColor)
    {
        Id = id;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        BackgroundColor = backgroundColor ?? throw new ArgumentNullException(nameof(backgroundColor));
        IsDeleted = false;
        CreateDate = UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string title, string? description, string backgroundColor)
    {
        Title = title;
        Description = description;
        BackgroundColor = backgroundColor;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}