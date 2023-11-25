using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Messages.Commons.Notifications.Events
{
    public interface INotificationEventManager
    {
        void AddNotificationEvent<T>(T @event) where T : Event;
        void RemoveNotificationEvent<T>(T @event) where T : Event;
        Task SendAllNotificationEvents();
    }
}
