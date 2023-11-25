using NewNerdStore.Core.Messages.Commons.Notifications.Errors;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface INotificationMediatorStrategy
    {
        Task PublishNotification<T>(T notification) where T : DomainErrorNotifications;
    }
}
