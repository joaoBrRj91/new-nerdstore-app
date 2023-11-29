using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public abstract class BaseCommandHandler<T> where T : Command
    {
        private readonly INotificationMediatorStrategy _notificationMediatorStrategy;

        protected BaseCommandHandler(INotificationMediatorStrategy notificationMediatorStrategy)
        {
            _notificationMediatorStrategy = notificationMediatorStrategy;
        }


        protected void PublishDomainErrorNotification(DomainErrorNotifications domainErrorNotification)
            => _notificationMediatorStrategy.PublishNotification(domainErrorNotification);


        protected bool CommandIsValid(T message)
        {
            if (message.EhValido()) return true;

            message.ValidationResult
                .Errors
                .ForEach(error =>
                {
                    //Lançar um evento de erro - Domain Notification
                    PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: message.MessageType, value: error.ErrorMessage));
                });


            return false;
        }

    }
}
