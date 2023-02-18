using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Trackers;

public sealed record TrackerQuery(Guid Id) : IRequest<TrackerView>;