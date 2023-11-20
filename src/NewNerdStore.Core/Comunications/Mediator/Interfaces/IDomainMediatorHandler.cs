using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Interfaces
{
    public interface IDomainMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
