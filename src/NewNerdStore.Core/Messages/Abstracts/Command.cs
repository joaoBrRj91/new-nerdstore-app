using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Core.Messages.Abstracts
{
    public abstract class Command : Message, IRequest<bool>
    {
        public Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
