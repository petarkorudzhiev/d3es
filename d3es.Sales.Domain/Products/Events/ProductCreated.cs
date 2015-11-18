using d3es.Sales.Domain.Products;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Products.Events
{
    public class ProductCreated : IEvent
    {
        public ProductCreated(Guid id, string name, Money price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
    }
}
