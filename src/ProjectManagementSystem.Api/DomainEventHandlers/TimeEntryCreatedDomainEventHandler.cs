// using MediatR;
// using ProjectManagementSystem.Domain.Activities;
// using ProjectManagementSystem.Domain.Issues.DomainEvents;
//
// namespace ProjectManagementSystem.Api.DomainEventHandlers;
//
// public sealed class TimeEntryCreatedDomainEventHandler : INotificationHandler<TimeEntryCreatedDomainEvent>
// {
//     private readonly IActivityRepository _activityRepository;
//
//     public TimeEntryCreatedDomainEventHandler(IActivityRepository activityRepository)
//     {
//         _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
//     }
//
//     public async Task Handle(TimeEntryCreatedDomainEvent notification, CancellationToken cancellationToken)
//     {
//         Activity? activity = await _activityRepository.Get(notification.Id, cancellationToken);
//
//         if (activity != null)
//             return;
//
//         await _activityRepository.Save(new Activity(notification.Id, notification.Content), cancellationToken);
//     }
// }