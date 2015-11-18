using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Cart
{
    public class RemoveProductFromCart : ICommand
    {
        public RemoveProductFromCart(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }

        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
