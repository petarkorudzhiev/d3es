using d3es.Sales.Domain.Products;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts.Events
{
    public class ProductAddedToCart : IEvent
    {
        public ProductAddedToCart(Guid customerId, Guid productId, Quantity quantity, Money price)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public Quantity Quantity { get; private set; }
        public Money Price { get; private set; }
    }
}
