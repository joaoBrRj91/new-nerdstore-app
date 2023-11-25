using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;

namespace NewNerdStore.Core.Comunications.Mediator.Implementations
{
    public class NotificationMediatorStrategy : INotificationMediatorStrategy
    {
        private readonly IMediator _mediator;

        public NotificationMediatorStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishNotification<T>(T notification) where T : DomainErrorNotifications
        {
            await _mediator.Publish(notification);
        }
    }
}
