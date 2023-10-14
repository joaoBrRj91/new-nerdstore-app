using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Core.Events
{
    public abstract class Event : Message, INotification
    {
        protected Event() => Timestamp = DateTime.Now;

        public DateTime Timestamp { get; private set; }
    }
}
