using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectTimeEntries
{
    public sealed class TimeEntryQuery : IRequest<TimeEntryViewModel>
    {
        public Guid ProjectId { get; }
        public Guid TimeEntryId { get; }

        public TimeEntryQuery(Guid projectId, Guid timeEntryId)
        {
            ProjectId = projectId;
            TimeEntryId = timeEntryId;
        }
    }
}