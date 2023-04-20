using MediatR;

namespace ProjectManagementSystem.Queries.User.IssueLabels;

public sealed record LabelListQuery(Guid IssueId) : IRequest<IEnumerable<LabelListItemViewModel>>;