using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Cart
{
    public class RemoveProductViewModel
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}