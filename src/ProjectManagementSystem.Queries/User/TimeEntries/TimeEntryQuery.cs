using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.TimeEntries
{
    public sealed class TimeEntryQuery : IRequest<TimeEntryView>
    {
        public Guid Id { get; }

        public TimeEntryQuery(Guid id)
        {
            Id = id;
        }
    }
}