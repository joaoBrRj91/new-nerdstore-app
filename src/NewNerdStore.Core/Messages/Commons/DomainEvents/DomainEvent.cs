using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Messages.Commons.DomainEvents
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
