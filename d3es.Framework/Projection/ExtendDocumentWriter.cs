using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Projection
{
    public static class ExtendDocumentWriter
    {
        public static TEntity AddOrUpdate<TKey, TEntity>(this IDocumentWriter<TKey, TEntity> self, TKey key, Func<TEntity> addFactory, Action<TEntity> update)
        {
            return self.AddOrUpdate(key, addFactory, entity =>
            {
                update(entity);
                return entity;
            });
        }

        public static TEntity AddOrUpdate<TKey, TEntity>(this IDocumentWriter<TKey, TEntity> self, TKey key, TEntity newView, Action<TEntity> updateViewFactory)
        {
            return self.AddOrUpdate(key, () => newView, view =>
            {
                updateViewFactory(view);
                return view;
            });
        }
        public static TEntity Add<TKey, TEntity>(this IDocumentWriter<TKey, TEntity> self, TKey key, TEntity newEntity)
        {
            return self.AddOrUpdate(key, newEntity, e =>
            {
                var txt = String.Format("Entity '{0}' with key '{1}' should not exist.", typeof(TEntity).Name, key);
                throw new InvalidOperationException(txt);
            });
        }
        public static TEntity UpdateOrThrow<TKey, TEntity>(this IDocumentWriter<TKey, TEntity> self, TKey key, Func<TEntity, TEntity> change)
        {
            return self.AddOrUpdate(key, () =>
            {
                var txt = String.Format("Failed to load '{0}' with key '{1}'.", typeof(TEntity).Name, key);
                throw new InvalidOperationException(txt);
            }, change);
        }
        public static TEntity UpdateOrThrow<TKey, TEntity>(this IDocumentWriter<TKey, TEntity> self, TKey key, Action<TEntity> change)
        {
            return self.AddOrUpdate(key, () =>
            {
                var txt = String.Format("Failed to load '{0}' with key '{1}'.", typeof(TEntity).Name, key);
                throw new InvalidOperationException(txt);
            }, change);
        }
        public static TView UpdateEnforcingNew<TKey, TView>(this IDocumentWriter<TKey, TView> self, TKey key,
            Action<TView> update)
            where TView : new()
        {
            return self.AddOrUpdate(key, () =>
            {
                var view = new TView();
                update(view);
                return view;
            }, v =>
            {
                update(v);
                return v;
            });
        }
    }
}
