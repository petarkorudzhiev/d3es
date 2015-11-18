using d3es.Framework.EventStore.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Products
{
    [Serializable]
    public class ProductMemento : IMemento
    {
        public ProductMemento(Guid id, int version, string name, decimal price)
        {
            Id = id;
            Version = version;
            Name = name;
            Price = price;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
