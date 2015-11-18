using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers.Events
{
    public class CustomerDeliveryAddressChanged : IEvent
    {
        public CustomerDeliveryAddressChanged(Guid customerId, DeliveryAddress address)
        {
            CustomerId = customerId;
            Address = address;
        }

        public Guid CustomerId { get; private set; }
        public DeliveryAddress Address { get; private set; }
    }
}
