using d3es.Framework.Events;
using d3es.Sales.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts.Events
{
    public class CartCheckedOut : IEvent
    {
        public CartCheckedOut(Guid customerId, DeliveryAddress address, Phone phone)
        {
            CustomerId = customerId;
            Address = address;
            Phone = phone;
        }

        public Guid CustomerId { get; private set; }
        public DeliveryAddress Address { get; private set; }
        public Phone Phone { get; private set; }
    }
}
