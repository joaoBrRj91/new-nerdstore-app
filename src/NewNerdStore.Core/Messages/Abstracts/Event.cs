using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Core.Messages.Abstracts
{
    public abstract class Event : Message, INotification
    {
        protected Event() => Timestamp = DateTime.Now;

        public DateTime Timestamp { get; private set; }
    }
}
