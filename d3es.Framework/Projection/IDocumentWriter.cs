using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public interface IDocumentWriter<in TKey, TEntity>
    {
        TEntity AddOrUpdate(TKey key, Func<TEntity> addFactory, Func<TEntity, TEntity> update);
        bool TryDelete(TKey key);
    }
}
