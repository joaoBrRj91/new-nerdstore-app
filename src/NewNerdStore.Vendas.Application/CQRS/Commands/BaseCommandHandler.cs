using NewNerdStore.Core.Events.Types;

namespace NewNerdStore.Vendas.Application.CQRS.Commands
{
    public abstract class BaseCommandHandler<T> where  T : Command
    {

        protected BaseCommandHandler()
        {
            
        }

        protected bool ValidarComando(T message)
        {
            if (message.EhValido()) return true;

            message.ValidationResult
                .Errors
                .ForEach(error =>
                {
                    //Lançar um evento de erro - Domain Notification
                });


            return false;
        }
    }
}
