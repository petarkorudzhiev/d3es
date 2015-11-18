using d3es.Sales.Domain.Products;
using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Cart
{
    public class AddProductToCart : ICommand
    {
        public AddProductToCart(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}
