using d3es.Sales.Domain.Products;
using d3es.Sales.Domain.Vouchers;
using d3es.Framework.Commands;
using d3es.Framework.Domain.Persistence;
using d3es.Sales.CommandStack.Commands.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.CommandHandlers
{
    public class VoucherHandlers : ICommandHandler<CreateVoucher>
    {
        private ISession _session;
        private IVoucherCodeGenerator _codeGenerator;

        public VoucherHandlers(ISession session, IVoucherCodeGenerator codeGenerator)
        {
            _session = session;
            _codeGenerator = codeGenerator;
        }

        public void Handle(CreateVoucher message)
        {
            var voucher = Voucher.CreateVoucher(message.CustomerId, new Money(message.Amount), _codeGenerator);
            _session.Add<Voucher>(voucher);

            _session.Commit();
        }
    }
}
