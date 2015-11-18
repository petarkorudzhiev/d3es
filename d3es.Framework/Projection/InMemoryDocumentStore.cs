using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public class InMemoryDocumentStore : IDocumentStore
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> _store = new ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>>();
        private readonly IDocumentStrategy _strategy;

        public InMemoryDocumentStore(IDocumentStrategy strategy)
        {
            _strategy = strategy;
        }

        public IDocumentWriter<TKey, TEntity> GetWriter<TKey, TEntity>() where TEntity : class
        {
            var bucket = _strategy.GetEntityBucket<TEntity>();
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return new MemoryDocumentReaderWriter<TKey, TEntity>(_strategy, store);
        }
        public IDocumentReader<TKey, TEntity> GetReader<TKey, TEntity>()
        {
            var bucket = _strategy.GetEntityBucket<TEntity>();
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return new MemoryDocumentReaderWriter<TKey, TEntity>(_strategy, store);
        }
        public IDocumentStrategy Strategy
        {
            get { return _strategy; }
        }
        public IEnumerable<DocumentRecord> EnumerateContents(string bucket)
        {
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return store.Select(p => new DocumentRecord(p.Key, () => p.Value)).ToArray();
        }
        public void WriteContents(string bucket, IEnumerable<DocumentRecord> records)
        {
            var pairs = records.Select(r => new KeyValuePair<string, byte[]>(r.Key, r.Read())).ToArray();
            _store[bucket] = new ConcurrentDictionary<string, byte[]>(pairs);
        }
        public void Reset(string bucket)
        {
            _store.Clear();
        }
    }
}
