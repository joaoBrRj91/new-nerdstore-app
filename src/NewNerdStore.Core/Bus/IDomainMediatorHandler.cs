using NewNerdStore.Core.Events;

namespace NewNerdStore.Core.Bus
{
    public interface IDomainMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
