using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public interface IDocumentReader<in TKey, TEntity>
    {
        bool TryGet(TKey key, out TEntity entity);
    }
}
