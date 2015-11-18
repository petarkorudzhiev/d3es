using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore.Snapshots
{
    public class Snapshot
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public IMemento State { get; set; }
    }
}
