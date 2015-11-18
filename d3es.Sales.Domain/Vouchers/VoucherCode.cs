using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Vouchers
{
    public class VoucherCode : ValueObject<VoucherCode>
    {
        private string _code;

        public VoucherCode(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException("code");

            // Check voucher code schema

            _code = code;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { _code };
        }

        public override string ToString()
        {
            return _code;
        }
    }
}
