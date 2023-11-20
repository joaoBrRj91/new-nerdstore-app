using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;

namespace NewNerdStore.WebApp.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IDomainNotificationErrosHandler _domainNotificationErrrosHandler;
        private readonly INotificationMediatorHandler _notificationMediatorHandler;

        public BaseController(INotificationHandler<DomainNotification> domainNotificationErrrosHandler, 
            INotificationMediatorHandler notificationMediatorHandler)
        {
            TokenClienteId = Guid.Parse("694fcb65-5891-4171-bf58-36bae4e645b9");
            _domainNotificationErrrosHandler = (IDomainNotificationErrosHandler)domainNotificationErrrosHandler;
            _notificationMediatorHandler = notificationMediatorHandler;
        }

        protected Guid TokenClienteId { get; private set; }

        protected bool OperacaoValida() => !_domainNotificationErrrosHandler.TemNotificacao();

        protected void NotificarErro(string key, string mensagem) =>
            _notificationMediatorHandler.PublishNotification(new DomainNotification(key, value: mensagem));

        protected IEnumerable<string> ObterMensagensErro() =>
            _domainNotificationErrrosHandler.ObterNotificacoes().Select(m => m.Value).ToList();
    }
}
