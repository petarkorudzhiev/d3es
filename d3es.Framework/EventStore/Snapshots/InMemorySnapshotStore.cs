using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore.Snapshots
{
    public class InMemorySnapshotStore : ISnapshotStore
    {
        private readonly ConcurrentDictionary<Guid, Snapshot> _inMemoryDB = new ConcurrentDictionary<Guid, Snapshot>();

        public Snapshot Get(Guid id)
        {
            Snapshot snapshot = null;
            _inMemoryDB.TryGetValue(id, out snapshot);

            return snapshot;
        }
        public void Save(Snapshot snapshot)
        {
            if (_inMemoryDB.ContainsKey(snapshot.AggregateId))
                _inMemoryDB[snapshot.AggregateId] = snapshot;
            else
                _inMemoryDB.TryAdd(snapshot.AggregateId, snapshot);
        }
    }
}
