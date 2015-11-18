using d3es.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.Commands.Cart
{
    public class CheckoutCart : ICommand
    {
        public CheckoutCart(Guid cartId, string city, string address, string phone)
        {
            CartId = cartId;
            City = city;
            Address = address;
            Phone = phone;
        }

        public Guid CartId { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
    }
}
