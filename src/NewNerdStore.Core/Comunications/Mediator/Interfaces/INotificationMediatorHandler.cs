using NewNerdStore.Core.Messages.Commons.Notifications;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface INotificationMediatorHandler
    {
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
