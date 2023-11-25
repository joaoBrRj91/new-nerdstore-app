using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;

namespace NewNerdStore.Vendas.Application.Comunication.Commands
{
    public abstract class BaseCommandHandler<T> where T : Command
    {
        private readonly INotificationMediatorStrategy _notificationMediatorHandler;

        protected BaseCommandHandler(INotificationMediatorStrategy notificationMediatorHandler)
        {
            _notificationMediatorHandler = notificationMediatorHandler;
        }

        protected bool CommandIsValid(T message)
        {
            if (message.EhValido()) return true;

            message.ValidationResult
                .Errors
                .ForEach(error =>
                {
                    //Lançar um evento de erro - Domain Notification
                    _notificationMediatorHandler
                    .PublishNotification(new DomainErrorNotifications(key: message.MessageType, value: error.ErrorMessage));
                });


            return false;
        }        

    }
}
