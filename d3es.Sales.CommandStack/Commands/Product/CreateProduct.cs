using d3es.Sales.Domain.Products;
using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Product
{
    public class CreateProduct : ICommand
    {
        public CreateProduct(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
