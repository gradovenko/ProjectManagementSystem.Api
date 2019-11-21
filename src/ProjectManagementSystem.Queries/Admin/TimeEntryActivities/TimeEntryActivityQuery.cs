using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities
{
    public class TimeEntryActivityQuery : IRequest<TimeEntryActivityViewModel>
    {
        public Guid Id { get; }

        public TimeEntryActivityQuery(Guid id)
        {
            Id = id;
        }
    }
}