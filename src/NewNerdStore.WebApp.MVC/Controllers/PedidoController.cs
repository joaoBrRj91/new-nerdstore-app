using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Vendas.Application.ComunicationBridge.Queries.Pedido;
using NewNerdStore.WebApp.MVC.Controllers;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly IPedidoQuery _pedidoQueries;

        public PedidoController(IPedidoQuery pedidoQueries,
            INotificationHandler<DomainErrorNotifications> notifications) 
            : base(notifications)
        {
            _pedidoQueries = pedidoQueries;
        }

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQueries.ObterPedidosCliente(TokenClienteId));
        }
    }
}