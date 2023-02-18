using MediatR;

namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

public sealed record TimeEntryActivityQuery(Guid Id) : IRequest<TimeEntryActivityView>;