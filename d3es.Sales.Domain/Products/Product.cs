using d3es.Framework.Domain;
using d3es.Framework.EventStore.Snapshots;
using d3es.Sales.Domain.Products;
using d3es.Sales.Domain.Products.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Products
{
    public class Product : AggregateRoot
    {
        public Product(string name, Money price)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Apply(new ProductCreated(Guid.NewGuid(), name, price));
        }

        protected Product() { }

        public string Name { get; private set; }
        public Money Price { get; private set; }

        private void When(ProductCreated e)
        {
            Id = e.Id;
            Name = e.Name;
            Price = e.Price;
        }

        protected override IMemento CreateMemento()
        {
            return new ProductMemento(Id, Version, Name, Price.Value);
        }
        protected override void SetMemento(IMemento memento)
        {
            var productMemento = (ProductMemento)memento;

            Id = productMemento.Id;
            Version = productMemento.Version;
            Name = productMemento.Name;
            Price = new Money(productMemento.Price);
        }
    }
}
