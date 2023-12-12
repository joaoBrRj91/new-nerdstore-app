using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Implementations;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoAtualizarItemCommandHandler : BaseCommandHandler<AtualizarItemPedidoCommand>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>, IDisposable
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEventMediatorStrategy _eventMediatorStrategy;

        public PedidoAtualizarItemCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationMediatorStrategy notificationMediatorStrategy,
            IEventMediatorStrategy eventMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
            _eventMediatorStrategy = eventMediatorStrategy;
        }

        //TODO : REFACTORING EXTRACT METHOD PRIVATE BUSINESS RULES SEGREGATIONS
        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                PublishDomainErrorNotification
                   (new DomainErrorNotifications(key: "Pedido", value: "Pedido não encontrado!"));

                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: "Pedido", value: "Item do pedido não encontrado!"));
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);

           await  _eventMediatorStrategy
                .PublishEvent(new PedidoProdutoAtualizadoDomainEvent(message.ClienteId, pedido.Id,message.ProdutoId, message.Quantidade));

            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
