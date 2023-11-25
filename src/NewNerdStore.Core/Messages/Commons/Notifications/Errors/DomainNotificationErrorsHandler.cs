using MediatR;

namespace NewNerdStore.Core.Messages.Commons.Notifications.Errors
{
    public class DomainNotificationErrorsHandler : IDomainNotificationErrosHandler, INotificationHandler<DomainErrorNotifications>
    {
        private List<DomainErrorNotifications> _notificationsErrors;

        public DomainNotificationErrorsHandler()
        {
            _notificationsErrors = new List<DomainErrorNotifications>();
        }

        public Task Handle(DomainErrorNotifications message, CancellationToken cancellationToken)
        {
            _notificationsErrors.Add(message);
            return Task.CompletedTask;
        }

        public List<DomainErrorNotifications> ObterNotificacoes() => _notificationsErrors;

        public bool TemNotificacao() => ObterNotificacoes().Any();

        public void Dispose() => _notificationsErrors.Clear();
    }
}
