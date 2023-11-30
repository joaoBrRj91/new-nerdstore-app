using NewNerdStore.Core.DomainObjects.Dtos;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents.Abstracts;

namespace NewNerdStore.Core.Messages.Commons.IntegrationEvents
{
    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public PedidoIniciadoEvent(
            Guid pedidoId,
            Guid clienteId,
            PedidoProdutosItemsDto pedidoProdutosItems,
            decimal total,
            string nomeCartao,
            string numeroCartao,
            string expiracaoCartao,
            string cvvCartao)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            PedidoProdutosItems = pedidoProdutosItems;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Total { get; private set; }
        public PedidoProdutosItemsDto PedidoProdutosItems { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

    }
}
