using d3es.Sales.Domain.Products;
using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Voucher
{
    public class CreateVoucher : ICommand
    {
        public CreateVoucher(Guid customerId, decimal amount)
        {
            CustomerId = customerId;
            Amount = amount;
        }

        public Guid CustomerId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
