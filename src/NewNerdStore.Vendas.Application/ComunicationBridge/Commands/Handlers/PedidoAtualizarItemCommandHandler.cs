using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoAtualizarItemCommandHandler : BaseCommandHandler<AtualizarItemPedidoCommand>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>, IDisposable
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoAtualizarItemCommandHandler(
            IPedidoRepository pedidoRepository,
            INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if(pedido == null)
            {
                 PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: "Pedido", value: "Pedido não encontrado!"));

                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if(!pedido.PedidoItemExistente(pedidoItem))
            {
                PublishDomainErrorNotification
                    (new DomainErrorNotifications(key: "Pedido", value: "Item do pedido não encontrado!"));
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);

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
