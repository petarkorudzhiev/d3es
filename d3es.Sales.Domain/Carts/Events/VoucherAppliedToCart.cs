using d3es.Sales.Domain.Products;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts.Events
{
    public class VoucherAppliedToCart : IEvent
    {
        public VoucherAppliedToCart(Guid customerId, Guid voucherId, Money amount)
        {
            CustomerId = customerId;
            VoucherId = voucherId;
            Amount = amount;
        }

        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }
        public Money Amount { get; private set; }
    }
}
