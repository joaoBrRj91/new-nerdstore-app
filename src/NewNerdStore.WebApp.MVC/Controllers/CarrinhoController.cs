using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Catalogos.Application.Mappers.AutoMapper;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications;
using NewNerdStore.Vendas.Application.Comunication.Commands;

namespace NewNerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly ICommandMediatorHandler _commandMediatorHandler;

        public CarrinhoController(
            IProdutoAppService produtoAppService,
            ICommandMediatorHandler commandMediatorHandler,
            INotificationHandler<DomainNotification> notificationHandler,
            INotificationMediatorHandler notificationMediator) : base(notificationHandler, notificationMediator)
        {
            _produtoAppService = produtoAppService;
            _commandMediatorHandler = commandMediatorHandler;
        }

        public IActionResult Index()
        {
            return View();
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
            var isCommandChangeStatusEntity =  await _commandMediatorHandler.Send(command);


            if (OperacaoValida())
                return RedirectToAction("Index");


            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new {produtoId});

        }
    }
}
