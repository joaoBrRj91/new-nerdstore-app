using MediatR;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Data.EventSourcing;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.IntegrationEvents.Abstracts;

namespace NewNerdStore.Core.Comunications.Mediator.Implementations
{
    public class EventMediatorStrategy : IEventMediatorStrategy
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventMediatorStrategy(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);

            if (@event.GetType().BaseType.Name.Equals(nameof(IntegrationEvent)))
                await _eventSourcingRepository.SalvarEvento(@event);
        }
    }
}
