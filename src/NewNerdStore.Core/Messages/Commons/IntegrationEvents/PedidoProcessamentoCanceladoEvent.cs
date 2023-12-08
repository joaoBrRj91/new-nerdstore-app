using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents.Abstracts;
using System;

namespace NewNerdStore.Core.Messages.Commons.IntegrationEvents
{
    public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public PedidoProdutosItemsDto ProdutosPedido { get; private set; }

        public PedidoProcessamentoCanceladoEvent(Guid pedidoId, Guid clienteId, PedidoProdutosItemsDto produtosPedido)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ProdutosPedido = produtosPedido;
        }
    }
}