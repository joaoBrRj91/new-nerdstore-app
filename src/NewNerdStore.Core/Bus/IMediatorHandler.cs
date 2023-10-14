using NewNerdStore.Core.Events;

namespace NewNerdStore.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
