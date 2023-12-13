using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoreEvent>> ObterEventos(Guid aggregateId);
    }
}
