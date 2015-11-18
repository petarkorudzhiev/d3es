using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public sealed class DocumentStrategy : IDocumentStrategy
    {
        public void Serialize<TEntity>(TEntity entity, Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, entity);
        }
        public TEntity Deserialize<TEntity>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();

            return (TEntity)formatter.Deserialize(stream);
        }
        public string GetEntityBucket<TEntity>()
        {
            return typeof(TEntity).Name;
        }
        public string GetEntityLocation<TEntity>(object key)
        {
            return key.ToString();
        }
    }
}
