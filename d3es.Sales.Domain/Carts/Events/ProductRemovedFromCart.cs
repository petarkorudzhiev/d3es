using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts.Events
{
    public class ProductRemovedFromCart : IEvent
    {
        public ProductRemovedFromCart(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
