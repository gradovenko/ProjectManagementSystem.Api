using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivityQuery : IRequest<TimeEntryActivityView>
    {
        public Guid Id { get; }

        public TimeEntryActivityQuery(Guid id)
        {
            Id = id;
        }
    }
}