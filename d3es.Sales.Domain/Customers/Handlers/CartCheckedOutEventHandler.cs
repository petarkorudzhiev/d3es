using d3es.Framework.Domain.Persistence;
using d3es.Framework.Events;
using d3es.Sales.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers.Handlers
{
    public class CartCheckedOutEventHandler : IEventHandler<CartCheckedOut>
    {
        private readonly IRepository _repository;

        public CartCheckedOutEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CartCheckedOut message)
        {
            var customer = _repository.Get<Customer>(message.CustomerId);

            customer.SetDeliveryAddress(message.Address);
            customer.SetPhone(message.Phone);

            _repository.Save<Customer>(customer);            
        }
    }
}
