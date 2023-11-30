using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface IEventMediatorStrategy
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
