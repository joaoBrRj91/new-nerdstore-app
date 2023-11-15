using NewNerdStore.Core.Events.Bases;

namespace NewNerdStore.Core.Events.Types
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
