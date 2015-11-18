using d3es.Sales.Domain.Products;
using d3es.Framework.Domain;
using d3es.Framework.EventStore.Snapshots;
using d3es.Sales.Domain.Vouchers;
using d3es.Sales.Domain.Vouchers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3es.Sales.Domain.Vouchers.Exceptions;

namespace d3es.Sales.Domain.Vouchers
{
    public class Voucher : AggregateRoot
    {
        public static Voucher CreateVoucher(Guid customerId, Money amount, IVoucherCodeGenerator codeGenerator)
        {
            // Validate domain invariants
            if (customerId == Guid.Empty)
                throw new ArgumentNullException("customerId");

            var voucher = new Voucher();
            voucher.Apply(new VoucherCreated(Guid.NewGuid(), customerId, VoucherStatus.Created, amount, codeGenerator.GenerateCode(), DateTime.UtcNow.AddDays(14)));

            return voucher;
        }
        protected Voucher() { }

        public Guid CustomerId { get; private set; }
        public VoucherCode Code { get; private set; }
        public VoucherStatus Status { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public Money Amount { get; private set; }

        public bool IsValid()
        {
            return Status == VoucherStatus.Created && ExpirationDate > DateTime.UtcNow;
        }
        public void MarkAsUsed()
        {
            if (!IsValid())
                throw new InvalidVoucherException();

            Status = VoucherStatus.Used;
        }

        private void When(VoucherCreated e)
        {
            Id = e.Id;
            CustomerId = e.CustomerId;
            Code = e.Code;
            Status = e.Status;
            ExpirationDate = e.ExpirationDate;
            Amount = e.Amount;
        }

        protected override IMemento CreateMemento()
        {
            return new VoucherMemento(Id, Version, CustomerId, Code.ToString(), (int)Status, ExpirationDate, Amount.Value);
        }
        protected override void SetMemento(IMemento memento)
        {
            var voucherMemento = (VoucherMemento)memento;

            Id = voucherMemento.Id;
            Version = voucherMemento.Version;
            CustomerId = voucherMemento.CustomerId;
            Code = new VoucherCode(voucherMemento.Code);
            Status = (VoucherStatus)voucherMemento.Status;
            ExpirationDate = voucherMemento.ExpirationDate;
            Amount = new Money(voucherMemento.Amount);
        }
    }
}
