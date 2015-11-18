using d3es.Sales.Domain.Carts;
using d3es.Sales.Domain.Products;
using d3es.Framework.Domain;
using d3es.Sales.Domain.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3es.Framework.EventStore.Snapshots;
using d3es.Sales.Domain.Purchases.Events;

namespace d3es.Sales.Domain.Purchases
{
    public class Purchase : AggregateRoot
    {
        private List<PurchaseItem> _items;

        public Purchase(Cart cart)
        {
            // TODO: Validate domain invariants

            var items = new List<PurchaseItem>();
            foreach (var item in cart.Items)
            {
                items.Add(new PurchaseItem(item.ProductId, item.Price, item.Quantity));
            }

            Apply(new PurchaseCreated(cart.CustomerId, cart.VoucherId, cart.GetTotalPrice(), items));
        }
        protected Purchase() { }

        public Guid CustomerId { get; private set; }
        public Money TotalPrice { get; private set; }
        public Guid? VoucherId { get; private set; }
        public PurchaseStatus Status { get; private set; }
        public IReadOnlyCollection<PurchaseItem> Items { get { return _items.AsReadOnly(); } }


        private void When(PurchaseCreated e)
        {
            CustomerId = e.CustomerId;
            VoucherId = e.VoucherId;
            TotalPrice = e.TotalPrice;
            Status = PurchaseStatus.Created;
            _items = new List<PurchaseItem>();

            foreach(var item in e.Items)
            {
                _items.Add(item);
            }
        }

        protected override IMemento CreateMemento()
        {
            List<PurchaseItemMemento> items = new List<PurchaseItemMemento>();
            foreach (var item in Items)
            {
                items.Add(new PurchaseItemMemento(item.ProductId, item.Price.Value, item.Quantity.Value));
            }

            return new PurchaseMemento(Id, Version, CustomerId, TotalPrice.Value, VoucherId, (int)Status, items);
        }
        protected override void SetMemento(IMemento memento)
        {
            var purchaseMemento = (PurchaseMemento)memento;

            Id = purchaseMemento.Id;
            Version = purchaseMemento.Version;
            CustomerId = purchaseMemento.CustomerId;
            TotalPrice = new Money(purchaseMemento.TotalPrice);
            VoucherId = purchaseMemento.VoucherId;
            Status = (PurchaseStatus)purchaseMemento.Status;
            _items = new List<PurchaseItem>();

            foreach (var item in purchaseMemento.Items)
            {
                _items.Add(new PurchaseItem(item.ProductId, new Money(item.Price), new Quantity(item.Quantity)));
            }
        }
    }
}
