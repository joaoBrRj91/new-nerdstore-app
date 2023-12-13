using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing
{
    /// <summary>
    /// Service do event store para se conectar ao banco de eventos
    /// <description>Segundo a documentação deve ter uma conexão por instancia da aplicação. Log deve ser singleton</description>
    /// </summary>
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection
                .Create(connectionString: configuration.GetConnectionString("EventStoreConnection"));

            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}
