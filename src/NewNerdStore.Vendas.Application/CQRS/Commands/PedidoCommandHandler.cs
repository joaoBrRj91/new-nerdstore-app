using MediatR;

namespace NewNerdStore.Vendas.Application.CQRS.Commands
{
    public class PedidoCommandHandler : BaseCommandHandler<AdicionarItemPedidoCommand>, IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            return true;
        }

    }
}
