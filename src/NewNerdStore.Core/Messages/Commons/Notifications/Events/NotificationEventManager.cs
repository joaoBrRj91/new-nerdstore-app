﻿using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Messages.Commons.Notifications.Events
{
    public class NotificationEventManager : INotificationEventManager
    {
        private List<Event> _notificationEvents;

        public IReadOnlyCollection<Event> NotificationEvents => _notificationEvents?.AsReadOnly();

        private readonly IDomainEventMediatorStrategy _domainEventMediatorHandler;

        public NotificationEventManager(IDomainEventMediatorStrategy domainEventMediatorHandler)
        {
            _notificationEvents = new List<Event>();
            _domainEventMediatorHandler = domainEventMediatorHandler;
        }

        public void AddNotificationEvent<T>(T @event) where T : Event 
            => _notificationEvents.Add(@event);


        public void RemoveNotificationEvent<T>(T @event) where T : Event 
            => _notificationEvents.Remove(@event);


        private void RemoveAllNotificationEvents() 
            => _notificationEvents.Clear();


        public async Task SendAllNotificationEvents()
        {
            if(NotificationEvents.Count == 0)
            {
                 await Task.CompletedTask;
                return;
            }


            var notificationEventsTasks = NotificationEvents
                .Select(async (@event) =>
            {
                await _domainEventMediatorHandler.PublishEvent(@event);
            });

            await Task.WhenAll(notificationEventsTasks);

            RemoveAllNotificationEvents();
        }

    }
}
