using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Vendas.Application.ComunicationBridge.Queries.Pedido;

namespace NewNerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPedidoQuery _pedidoQuery;

        // TODO: Obter cliente logado
        protected Guid ClienteId = Guid.Parse("694fcb65-5891-4171-bf58-36bae4e645b9");


        public CartViewComponent(IPedidoQuery pedidoQuery)
        {
            _pedidoQuery = pedidoQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQuery.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Items.Count ?? 0;

            return View(itens);
        }
    }
}
