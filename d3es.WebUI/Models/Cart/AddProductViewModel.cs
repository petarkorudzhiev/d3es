using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Cart
{
    public class AddProductViewModel
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}