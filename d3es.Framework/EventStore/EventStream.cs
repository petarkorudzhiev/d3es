using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore
{
    public class EventStream
    {
        public EventStream(int version, List<IEvent> events)
        {
            Version = version;
            Events = events;
        }

        public int Version { get; private set; }
        public List<IEvent> Events { get; private set; }

        public void AppendEvents(int newVersion, IEnumerable<IEvent> newEvents)
        {
            Version = newVersion;
            Events.AddRange(newEvents);
        }
    }
}
