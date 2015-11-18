using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Voucher
{
    public class CreateVoucherViewModel
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}