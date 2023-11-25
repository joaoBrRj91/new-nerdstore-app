using NewNerdStore.Core.Messages.Commons.DomainEvents;

namespace NewNerdStore.Catalogos.Domain.Events
{
    public class ProdutoAbaixoEstoqueDomainEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }

        public ProdutoAbaixoEstoqueDomainEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
        }
    }
}
