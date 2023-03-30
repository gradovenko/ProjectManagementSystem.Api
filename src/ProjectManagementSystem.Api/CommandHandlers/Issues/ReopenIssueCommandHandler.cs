using MediatR;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class ReopenIssueCommandHandler : IRequestHandler<ReopenIssueCommand, ReopenIssueCommandResultState>
{
    public Task<ReopenIssueCommandResultState> Handle(ReopenIssueCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}