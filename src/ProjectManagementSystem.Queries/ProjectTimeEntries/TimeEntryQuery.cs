using MediatR;

namespace ProjectManagementSystem.Queries.ProjectTimeEntries;

public sealed record TimeEntryQuery(Guid ProjectId, Guid TimeEntryId) : IRequest<TimeEntryView>;