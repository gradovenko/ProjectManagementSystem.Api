using MediatR;

namespace ProjectManagementSystem.Queries.TimeEntries;

public sealed record TimeEntryQuery(Guid Id) : IRequest<TimeEntryView>;