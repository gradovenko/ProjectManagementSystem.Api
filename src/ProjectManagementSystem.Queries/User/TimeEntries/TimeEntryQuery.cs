using MediatR;

namespace ProjectManagementSystem.Queries.User.TimeEntries;

public sealed record TimeEntryQuery(Guid Id) : IRequest<TimeEntryView>;