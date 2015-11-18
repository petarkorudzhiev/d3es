using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore.Snapshots
{
    public class DefaultSnapshotStrategy : ISnapshotStrategy
    {
        private const int SnapshotInterval = 1;

        public bool ShouldMakeSnapShot(AggregateRoot aggregate)
        {
            var i = aggregate.Version;
            for (var j = 0; j < aggregate.UncommittedEvents.Count(); j++)
                if (++i % SnapshotInterval == 0 && i != 0) return true;
            return false;
        }
    }
}
