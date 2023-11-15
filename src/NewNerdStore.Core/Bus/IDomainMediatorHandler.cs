using NewNerdStore.Core.Events.Bases;

namespace NewNerdStore.Core.Bus
{
    public interface IDomainMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
