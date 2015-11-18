using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Cart
{
    public class ApplyVoucherToCart : ICommand
    {
        public ApplyVoucherToCart(Guid customerId, Guid voucherId)
        {
            CustomerId = customerId;
            VoucherId = voucherId;
        }

        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }
    }
}
