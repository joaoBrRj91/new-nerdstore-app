using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries.Dtos;
using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Vendas.Application.Comunication.Commands;
using NewNerdStore.Vendas.Application.ComunicationBridge.Commands;
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


        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new RemoverItemPedidoCommand(TokenClienteId, id);
            var isCommandChangeStatusEntity = await _commandMediatorStrategy.Send(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new AtualizarItemPedidoCommand(TokenClienteId, id, quantidade);
            var isCommandChangeStatusEntity = await _commandMediatorStrategy.Send(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(TokenClienteId, voucherCodigo);
            var isCommandChangeStatusEntity = await _commandMediatorStrategy.Send(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoDto carrinhoDto)
        {
            var carrinho = await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId);

            var command = new IniciarPedidoCommand(carrinho.PedidoId, TokenClienteId, carrinho.ValorTotal, carrinhoDto.Pagamento.NomeCartao,
                carrinhoDto.Pagamento.NumeroCartao, carrinhoDto.Pagamento.ExpiracaoCartao, carrinhoDto.Pagamento.CvvCartao);

            await _commandMediatorStrategy.Send(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Pedido");
            }

            return View("ResumoDaCompra", await _pedidoQuery.ObterCarrinhoCliente(TokenClienteId));
        }
    }
}
