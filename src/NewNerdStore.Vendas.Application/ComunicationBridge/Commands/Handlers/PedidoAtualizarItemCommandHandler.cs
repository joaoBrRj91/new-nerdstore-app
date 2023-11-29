using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public class PedidoAtualizarItemCommandHandler : BaseCommandHandler<AtualizarItemPedidoCommand>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>, IDisposable
    {

        public PedidoAtualizarItemCommandHandler(INotificationMediatorStrategy notificationMediatorStrategy)
            : base(notificationMediatorStrategy)
        {
        }

        public Task<bool> Handle(AtualizarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
