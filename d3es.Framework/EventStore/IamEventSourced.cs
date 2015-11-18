using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore
{
    public interface IamEventSourced
    {
        IEnumerable<IEvent> UncommittedEvents { get; }
        void MarkChangesAsCommitted();
        int Version { get; }
        void LoadHistory(IEnumerable<IEvent> events, int version);
    }
}
