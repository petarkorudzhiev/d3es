﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Vouchers
{
    public interface IVoucherCodeGenerator
    {
        VoucherCode GenerateCode();
    }
}
