using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Cart
{
    public class ApplyVoucherViewModel
    {
        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }
    }
}