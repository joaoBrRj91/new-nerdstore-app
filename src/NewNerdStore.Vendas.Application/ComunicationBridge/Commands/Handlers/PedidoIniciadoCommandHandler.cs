using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Extensions;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoIniciadoCommandHandler : BaseCommandHandler<IniciarPedidoCommand>,
        IRequestHandler<IniciarPedidoCommand, bool>, IDisposable
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly INotificationEventManager _notificationEventManager;


        public PedidoIniciadoCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationEventManager notificationEventManager,
            INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _notificationEventManager = notificationEventManager;
        }

        public async Task<bool> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            pedido.IniciarPedido();

            var itensList = new List<ProdutoItemDto>();
            pedido.PedidoItems.ForEach(i => itensList.Add(new ProdutoItemDto { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var pedidoProdutosItems = new PedidoProdutosItemsDto { PedidoId = pedido.Id, Itens = itensList };


            //TODO: Evento de integração de pedido iniciado para o contexto de catalogo [Fluxo de debitar estoque]
            _notificationEventManager
                .AddNotificationEvent(new PedidoIniciadoEvent(pedido.Id, pedido.ClienteId, pedidoProdutosItems, pedido.ValorTotal,
                message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));

            _pedidoRepository.Atualizar(pedido);

            var isCommandChangeStatusEntity = await _pedidoRepository.UnitOfWork.Commit();

            if (isCommandChangeStatusEntity)
                await _notificationEventManager.SendAllNotificationEvents();

            return isCommandChangeStatusEntity;
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
        }
    }
}
