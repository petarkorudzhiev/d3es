using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public interface IDocumentStore
    {
        IDocumentWriter<TKey, TEntity> GetWriter<TKey, TEntity>() where TEntity : class;
        IDocumentReader<TKey, TEntity> GetReader<TKey, TEntity>();
        IDocumentStrategy Strategy { get; }
        IEnumerable<DocumentRecord> EnumerateContents(string bucket);
        void WriteContents(string bucket, IEnumerable<DocumentRecord> records);
        void Reset(string bucket);
    }

    public sealed class DocumentRecord
    {
        public readonly string Key;
        public readonly Func<byte[]> Read;

        public DocumentRecord(string key, Func<byte[]> read)
        {
            Key = key;
            Read = read;
        }
    }
}
