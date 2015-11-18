using d3es.Framework.EventStore.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Vouchers
{
    [Serializable]
    public class VoucherMemento : IMemento
    {
        public VoucherMemento(Guid id, int version, Guid customerId, string code, int status, DateTime expirationDate, decimal amount)
        {
            Id = id;
            Version = version;
            CustomerId = customerId;
            Code = code;
            Status = status;
            ExpirationDate = expirationDate;
            Amount = amount;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Code { get; private set; }
        public int Status { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public decimal Amount { get; private set; }
    }
}
