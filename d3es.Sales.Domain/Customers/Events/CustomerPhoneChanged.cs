using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers.Events
{
    public class CustomerPhoneChanged : IEvent
    {
        public CustomerPhoneChanged(Guid customerId, Phone phone)
        {
            CustomerId = customerId;
            Phone = phone;
        }

        public Guid CustomerId { get; private set; }
        public Phone Phone { get; private set; }
    }
}
