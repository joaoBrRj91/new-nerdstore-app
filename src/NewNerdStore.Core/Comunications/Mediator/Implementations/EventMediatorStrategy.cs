using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Implementations
{
    public class EventMediatorStrategy : IEventMediatorStrategy
    {
        private readonly IMediator _mediator;

        public EventMediatorStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }
    }
}
