using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Vendas.Application.Comunication.Commands;
using NewNerdStore.Vendas.Application.ComunicationBridge.Queries.Pedido;

namespace NewNerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQuery _pedidoQuery;
        private readonly ICommandMediatorStrategy _commandMediatorStrategy;

        public CarrinhoController(
            IProdutoAppService produtoAppService,
            ICommandMediatorStrategy commandMediatorStrategy,
            IPedidoQuery  pedidoQuery,  
            INotificationHandler<DomainErrorNotifications> notificationHandler
            /*INotificationMediatorHandler notificationMediator*/) : base(notificationHandler/*, notificationMediator*/)
        {
            _produtoAppService = produtoAppService;
            _commandMediatorStrategy = commandMediatorStrategy;
            _pedidoQuery = pedidoQuery;
        }


        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }


        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid produtoId, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(produtoId);
            if (produto is null) return NotFound();


            //TODO : Refatorar para que o produtoAppService verifique a quantidade de estoque do produto
            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { produtoId });
            }

            var command = new AdicionarItemPedidoCommand(TokenClienteId, produtoId, produto.Nome, quantidade, produto.Valor);
            var isCommandChangeStatusEntity =  await _commandMediatorStrategy.Send(command);


            if (OperacaoValida())
                return RedirectToAction("Index");


            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new {produtoId});

        }
    }
}
