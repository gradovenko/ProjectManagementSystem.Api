using MediatR;

namespace ProjectManagementSystem.Queries.User.Labels;

public sealed record LabelListQuery(Guid projectId) : IRequest<IEnumerable<LabelListItemViewModel>>;