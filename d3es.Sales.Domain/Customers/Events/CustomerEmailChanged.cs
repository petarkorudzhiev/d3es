using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers.Events
{
    public class CustomerEmailChanged : IEvent
    {
        public CustomerEmailChanged(Guid customerId, Email email)
        {
            CustomerId = customerId;
            Email = email;
        }

        public Guid CustomerId { get; private set; }
        public Email Email { get; private set; }
    }
}
