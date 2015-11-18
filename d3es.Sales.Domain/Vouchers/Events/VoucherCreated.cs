using d3es.Sales.Domain.Products;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Vouchers.Events
{
    public class VoucherCreated : IEvent
    {
        public VoucherCreated(Guid id, Guid customerId, VoucherStatus status, Money amount, VoucherCode code, DateTime expirationDate)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            Amount = amount;
            Code = code;
            ExpirationDate = expirationDate;
        }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public VoucherStatus Status { get; private set; }
        public Money Amount { get; private set; }
        public VoucherCode Code { get; private set; }
        public DateTime ExpirationDate { get; private set; }
    }
}
