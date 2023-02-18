using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectTimeEntries;

public sealed record TimeEntryQuery(Guid ProjectId, Guid TimeEntryId) : IRequest<TimeEntryView>;