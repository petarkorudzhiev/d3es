using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Cart
{
    public class CheckoutViewModel
    {
        [Required]
        public Guid CartId { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
    }
}