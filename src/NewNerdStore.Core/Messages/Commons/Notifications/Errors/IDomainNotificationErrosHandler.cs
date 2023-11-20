namespace NewNerdStore.Core.Messages.Commons.Notifications.Errors
{
    public interface IDomainNotificationErrosHandler: IDisposable
    {
        List<DomainNotification> ObterNotificacoes();
        bool TemNotificacao();
    }
}
