using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Exceptions
{
    public class EventStoreConcurrencyException : Exception
    {
        public IEnumerable<IEvent> StoreEvents { get; private set; }
        public int StoreVersion { get; private set; }

        public EventStoreConcurrencyException(IEnumerable<IEvent> storeEvents, int storeVersion)
        {
            StoreEvents = storeEvents;
            StoreVersion = storeVersion;
        }
    }
}
