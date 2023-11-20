using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications;

namespace NewNerdStore.Core.Comunications.Mediator.Implementations
{
    public class NotificationMediatorHandler : INotificationMediatorHandler
    {
        private readonly IMediator _mediator;

        public NotificationMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
