using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public class MemoryDocumentReaderWriter<TKey, TEntity> : IDocumentReader<TKey, TEntity>, IDocumentWriter<TKey, TEntity>
    {
        private readonly ConcurrentDictionary<string, byte[]> _store;
        private readonly IDocumentStrategy _strategy;

        public MemoryDocumentReaderWriter(IDocumentStrategy strategy, ConcurrentDictionary<string, byte[]> store)
        {
            _strategy = strategy;
            _store = store;
        }

        public bool TryGet(TKey key, out TEntity entity)
        {
            var name = GetName(key);
            byte[] bytes;
            if (_store.TryGetValue(name, out bytes))
            {
                using (var mem = new MemoryStream(bytes))
                {
                    entity = _strategy.Deserialize<TEntity>(mem);
                    return true;
                }
            }
            entity = default(TEntity);
            return false;
        }
        public TEntity AddOrUpdate(TKey key, Func<TEntity> addFactory, Func<TEntity, TEntity> update)
        {
            var result = default(TEntity);
            _store.AddOrUpdate(GetName(key), s =>
            {
                result = addFactory();
                using (var memory = new MemoryStream())
                {
                    _strategy.Serialize(result, memory);
                    return memory.ToArray();
                }
            }, (s2, bytes) =>
            {
                TEntity entity;
                using (var memory = new MemoryStream(bytes))
                {
                    entity = _strategy.Deserialize<TEntity>(memory);
                }
                result = update(entity);
                using (var memory = new MemoryStream())
                {
                    _strategy.Serialize(result, memory);
                    return memory.ToArray();
                }
            });
            return result;
        }
        public bool TryDelete(TKey key)
        {
            byte[] bytes;
            return _store.TryRemove(GetName(key), out bytes);
        }

        private string GetName(TKey key)
        {
            return _strategy.GetEntityLocation<TEntity>(key);
        }
    }
}
