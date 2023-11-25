using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain
{
    public class PedidoItemAdicionadoDomainEvent : DomainEvent
    {
        public PedidoItemAdicionadoDomainEvent(
            Guid pedidoId, 
            Guid produtoId,
            string produtoNome, 
            Guid clienteId,
            decimal valorUnitario,
            int quantidade)
            : base(aggregateId: pedidoId)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            ClienteId = clienteId;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; set; }
        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }

    }
}
