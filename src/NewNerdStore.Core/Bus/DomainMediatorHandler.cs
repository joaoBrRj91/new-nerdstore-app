using MediatR;
using NewNerdStore.Core.Events.Bases;

namespace NewNerdStore.Core.Bus
{
    public class DomainMediatorHandler : IDomainMediatorHandler
    {
        private readonly IMediator _mediator;

        public DomainMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }
    }
}
