namespace ProjectManagementSystem.Domain.Issues;

public sealed class Project
{
    public Guid Id { get; private set; }
    
    private Project() { }
}