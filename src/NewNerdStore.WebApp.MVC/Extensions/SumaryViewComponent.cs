using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;

namespace NewNerdStore.WebApp.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly IDomainNotificationErrosHandler _notifications;

        public SummaryViewComponent(INotificationHandler<DomainErrorNotifications> notifications)
        {
            _notifications = (IDomainNotificationErrosHandler)notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return View();
        }
    }
}
