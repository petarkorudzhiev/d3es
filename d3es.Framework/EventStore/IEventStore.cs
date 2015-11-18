using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore
{
    public interface IEventStore
    {
        EventStream LoadEventStream(Guid id);
        EventStream LoadEventStream(Guid id, int skipEvents, int maxCount);
        void AppendToStream(Guid id, int expectedVersion, IEnumerable<IEvent> events);
    }
}
