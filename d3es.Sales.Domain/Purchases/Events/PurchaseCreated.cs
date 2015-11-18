using d3es.Framework.Events;
using d3es.Sales.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Purchases.Events
{
    public class PurchaseCreated : IEvent
    {
        public PurchaseCreated(Guid customerId, Guid? voucherId, Money totalPrice, IReadOnlyCollection<PurchaseItem> items)
        {
            CustomerId = customerId;
            VoucherId = voucherId;
            TotalPrice = totalPrice;

            Items = items;
        }

        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public Money TotalPrice { get; private set; }
        public IReadOnlyCollection<PurchaseItem> Items { get; private set; }
    }
}
