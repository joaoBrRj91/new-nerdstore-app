using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface IDomainEventMediatorStrategy
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
