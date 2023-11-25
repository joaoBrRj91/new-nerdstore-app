namespace NewNerdStore.Core.Messages.Commons.Notifications.Errors
{
    public interface IDomainNotificationErrosHandler: IDisposable
    {
        List<DomainErrorNotifications> ObterNotificacoes();
        bool TemNotificacao();
    }
}
