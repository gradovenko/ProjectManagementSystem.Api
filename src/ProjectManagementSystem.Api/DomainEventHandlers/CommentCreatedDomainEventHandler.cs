// using MediatR;
// using ProjectManagementSystem.Domain.Activities;
// using ProjectManagementSystem.Domain.Comments.DomainEvents;
//
// namespace ProjectManagementSystem.Api.DomainEventHandlers;
//
// public sealed class CommentCreatedDomainEventHandler : INotificationHandler<CommentCreatedDomainEvent>
// {
//     private readonly IActivityRepository _activityRepository;
//
//     public CommentCreatedDomainEventHandler(IActivityRepository activityRepository)
//     {
//         _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
//     }
//
//     public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
//     {
//         Activity? activity = await _activityRepository.Get(notification.CommentId, cancellationToken);
//
//         if (activity != null)
//             return;
//
//         await _activityRepository.Save(new Activity(notification.CommentId, notification.Content), cancellationToken);
//     }
// }