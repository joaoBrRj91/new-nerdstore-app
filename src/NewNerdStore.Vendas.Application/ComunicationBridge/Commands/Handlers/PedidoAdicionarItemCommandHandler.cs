using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Application.Comunication.Commands;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;
using NewNerdStore.Vendas.Domain.Entities;
using NewNerdStore.Vendas.Domain.Factories;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoAdicionarItemCommandHandler : BaseCommandHandler<AdicionarItemPedidoCommand>,
        IRequestHandler<AdicionarItemPedidoCommand, bool>, IDisposable
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly INotificationEventManager _notificationEventManager;

        public PedidoAdicionarItemCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationMediatorStrategy notificationMediatorStrategy,
            INotificationEventManager notificationEventManager)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _notificationEventManager = notificationEventManager;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.ProdutoNome, message.Quantidade, message.ValorUnitario);

            if (pedido is null)
                CriarNovoPedidoRascunho(pedidoItem, message.ClienteId);
            else
                GerenciarItemPedido(pedido, pedidoItem);

            var isCommandChangeStatusEntity = await _pedidoRepository.UnitOfWork.Commit();

            if (isCommandChangeStatusEntity)
                await _notificationEventManager.SendAllNotificationEvents();

            return isCommandChangeStatusEntity;

        }

        private void CriarNovoPedidoRascunho(PedidoItem pedidoItem, Guid clienteId)
        {
            var pedido = PedidoFactory.NovoPedidoRascunho(clienteId);

            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);

            _notificationEventManager
                .AddNotificationEvent(new PedidoRascunhoIniciadoDomainEvent(clienteId, pedidoItem.ProdutoId));

        }

        private void GerenciarItemPedido(Pedido pedido, PedidoItem pedidoItem)
        {
            var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if (pedidoItemExistente)
                _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
            else
            {
                _pedidoRepository.AdicionarItem(pedidoItem);
                _notificationEventManager
                .AddNotificationEvent(new PedidoRascunhoIniciadoDomainEvent(pedido.ClienteId, pedido.Id));
            }


            _notificationEventManager
                .AddNotificationEvent(new PedidoRascunhoAtualizadoDomainEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
        }


        public void Dispose() => _pedidoRepository.Dispose();

    }
}
