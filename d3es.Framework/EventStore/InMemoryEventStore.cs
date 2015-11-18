using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using d3es.Framework.Domain.Exceptions;

namespace d3es.Framework.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentDictionary<Guid, EventStream> _inMemoryDB = new ConcurrentDictionary<Guid, EventStream>();

        public EventStream LoadEventStream(Guid id)
        {
            EventStream stream;
            _inMemoryDB.TryGetValue(id, out stream);
            return stream != null ? stream : new EventStream(0, new List<IEvent>());
        }
        public EventStream LoadEventStream(Guid id, int skipEvents, int maxCount)
        {
            EventStream stream;
            _inMemoryDB.TryGetValue(id, out stream);
            if (stream != null)
            {
                return new EventStream(stream.Version, stream.Events.Skip(skipEvents).Take(maxCount).ToList());
            }
            else
            {
                return new EventStream(0, new List<IEvent>());
            }  
        }
        public void AppendToStream(Guid id, int expectedVersion, IEnumerable<IEvent> events)
        {
            EventStream stream;
            _inMemoryDB.TryGetValue(id, out stream);
            if (stream == null)
            {
                stream = new EventStream(expectedVersion, new List<IEvent>(events));
                _inMemoryDB.TryAdd(id, stream); 
            }
            else
            {
                if (stream.Version < expectedVersion)
                {
                    stream.AppendEvents(expectedVersion, events);
                }
                else
                {
                    int skipEvents = expectedVersion - events.Count();
                    throw new EventStoreConcurrencyException(stream.Events.Skip(skipEvents), stream.Version);
                }
            }
        }
    }
}
