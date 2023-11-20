using MediatR;

namespace NewNerdStore.Core.Messages.Commons.Notifications.Errors
{
    public class DomainNotificationErrorsHandler : IDomainNotificationErrosHandler, INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notificationsErrors;

        public DomainNotificationErrorsHandler()
        {
            _notificationsErrors = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notificationsErrors.Add(message);
            return Task.CompletedTask;
        }

        public List<DomainNotification> ObterNotificacoes() => _notificationsErrors;

        public bool TemNotificacao() => ObterNotificacoes().Any();

        public void Dispose() => _notificationsErrors.Clear();
    }
}
