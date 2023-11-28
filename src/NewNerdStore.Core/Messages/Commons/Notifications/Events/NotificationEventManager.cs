using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Messages.Commons.Notifications.Events
{
    public class NotificationEventManager : INotificationEventManager
    {
        private List<Event> _notificationEvents;

        public IReadOnlyCollection<Event> NotificationEvents => _notificationEvents?.AsReadOnly();

        private readonly IDomainEventMediatorStrategy _domainEventMediatorStrategy;

        public NotificationEventManager(IDomainEventMediatorStrategy domainEventMediatorStrategy)
        {
            _notificationEvents = new List<Event>();
            _domainEventMediatorStrategy = domainEventMediatorStrategy;
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
                await _domainEventMediatorStrategy.PublishEvent(@event);
            });

            await Task.WhenAll(notificationEventsTasks);

            RemoveAllNotificationEvents();
        }

    }
}
