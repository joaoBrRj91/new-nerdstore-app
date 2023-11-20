using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications;

namespace NewNerdStore.Vendas.Application.Comunication.Commands
{
    public abstract class BaseCommandHandler<T> where T : Command
    {
        private readonly INotificationMediatorHandler _notificationMediatorHandler;

        protected BaseCommandHandler(INotificationMediatorHandler notificationMediatorHandler)
        {
            _notificationMediatorHandler = notificationMediatorHandler;
        }

        protected bool ValidarComando(T message)
        {
            if (message.EhValido()) return true;

            message.ValidationResult
                .Errors
                .ForEach(error =>
                {
                    //Lançar um evento de erro - Domain Notification
                    _notificationMediatorHandler
                    .PublishNotification(new DomainNotification(key: message.MessageType, value: error.ErrorMessage));
                });


            return false;
        }
    }
}
