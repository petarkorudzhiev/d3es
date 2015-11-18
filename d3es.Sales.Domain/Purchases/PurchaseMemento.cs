using d3es.Framework.EventStore.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Purchases
{
    [Serializable]
    public class PurchaseMemento : IMemento
    {
        private List<PurchaseItemMemento> _items;

        public PurchaseMemento(Guid id, int version, Guid customerId, decimal totalPrice, Guid? voucherId, int status, List<PurchaseItemMemento> items)
        {
            Id = id;
            Version = version;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            VoucherId = voucherId;
            Status = status;
            _items = items;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid? VoucherId { get; private set; }
        public int Status { get; private set; }
        public IReadOnlyCollection<PurchaseItemMemento> Items
        {
            get { return _items.AsReadOnly(); }
        }
    }

    [Serializable]
    public class PurchaseItemMemento
    {
        public PurchaseItemMemento(Guid productId, decimal price, int quantity)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
