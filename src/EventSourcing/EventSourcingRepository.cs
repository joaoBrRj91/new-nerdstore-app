using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using NewNerdStore.Core.Data.EventSourcing;
using NewNerdStore.Core.Messages.Abstracts;
using Newtonsoft.Json;
using System.Text;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public  async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            await _eventStoreService
                .GetConnection()
                .AppendToStreamAsync(
                stream: evento.AggregateId.ToString(),
                expectedVersion: ExpectedVersion.Any,
                events: FormatarEvento(evento));
        }

        public async Task<IEnumerable<StoreEvent>> ObterEventos(Guid aggregateId)
        {
            var eventos = await _eventStoreService.GetConnection()
               .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

            var listaEventos = new List<StoreEvent>();

            foreach (var resolvedEvent in eventos.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<Event>(dataEncoded);

                var evento = new StoreEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listaEventos.Add(evento);
            }

            return listaEventos.OrderBy(e => e.DataOcorrencia);
        }

        private IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            yield return new EventData(
                   eventId: Guid.NewGuid(),
                   type: evento.MessageType,
                   isJson: true,
                   Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                   metadata: null
                );
        }
    }
}
