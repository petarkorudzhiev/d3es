using d3es.Framework.EventStore.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts
{
    [Serializable]
    public class CartMemento : IMemento
    {
        private List<CartItemMemento> _items;

        public CartMemento(Guid id, int version, Guid customerId, decimal? voucherAmount, Guid? voucherId, List<CartItemMemento> items)
        {
            Id = id;
            Version = version;
            CustomerId = customerId;
            VoucherAmount = voucherAmount;
            VoucherId = voucherId;
            _items = items;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal? VoucherAmount { get; private set; }
        public Guid? VoucherId { get; private set; }
        public IReadOnlyCollection<CartItemMemento> CartItems 
        {
            get { return _items.AsReadOnly(); }
        }
    }

    [Serializable]
    public class CartItemMemento
    {
        public CartItemMemento(Guid productId, int quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}
