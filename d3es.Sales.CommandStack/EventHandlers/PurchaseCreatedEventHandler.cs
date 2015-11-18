using d3es.Framework.Events;
using d3es.Sales.Domain.Purchases.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.EventHandlers
{
    public class PurchaseCreatedEventHandler : IEventHandler<PurchaseCreated>
    {
        public void Handle(PurchaseCreated message)
        {
            // Send external event to other Bounded Contexts using the data in the internal event.
            // Send only required information -> not everything
        }
    }
}
