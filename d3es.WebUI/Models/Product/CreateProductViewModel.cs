using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models.Product
{
    public class CreateProductViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}