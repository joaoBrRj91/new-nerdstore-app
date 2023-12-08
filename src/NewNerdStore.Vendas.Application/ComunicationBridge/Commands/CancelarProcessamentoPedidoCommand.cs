using System;
using NerdStore.Core.Messages;
using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands
{
    public class CancelarProcessamentoPedidoCommand : Command
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }
    }
}