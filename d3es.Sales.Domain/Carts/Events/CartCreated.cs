using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts.Events
{
    public class CartCreated : IEvent
    {
        public CartCreated(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}
