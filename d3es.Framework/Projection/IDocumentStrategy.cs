using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public interface IDocumentStrategy
    {
        string GetEntityBucket<TEntity>();
        string GetEntityLocation<TEntity>(object key);


        void Serialize<TEntity>(TEntity entity, Stream stream);
        TEntity Deserialize<TEntity>(Stream stream);
    }
}
