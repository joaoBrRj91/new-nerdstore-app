namespace NewNerdStore.Core.Data.EventSourcing
{
    public class StoreEvent
    {
        public StoreEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
        {
            Id = id;
            Tipo = tipo;
            DataOcorrencia = dataOcorrencia;
            Dados = dados;
        }

        public Guid Id { get; private set; }

        public string Tipo { get; private set; }

        public DateTime DataOcorrencia { get; private set; }

        public string Dados { get; private set; }

    }
}
