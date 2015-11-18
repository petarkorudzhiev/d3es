using d3es.Sales.Domain.Carts;
using d3es.Framework.Domain.Persistence;
using d3es.Framework.Events;
using d3es.Sales.Domain.Customers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers.Handlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreated>
    {
        private readonly IRepository _repository;

        public CustomerCreatedEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CustomerCreated message)
        {
            var cart = new Cart(message.Id);

            _repository.Save<Cart>(cart);
        }
    }
}
