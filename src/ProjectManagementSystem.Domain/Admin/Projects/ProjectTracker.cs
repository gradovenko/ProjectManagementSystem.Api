namespace ProjectManagementSystem.Domain.Admin.Projects;

public sealed class ProjectTracker
{
    public Guid ProjectId { get; }
    public Guid TrackerId { get; }

    public ProjectTracker(Guid projectId, Guid trackerId)
    {
        ProjectId = projectId;
        TrackerId = trackerId;
    }
}