using d3es.Sales.Domain.Customers;
using d3es.Framework.Commands;
using d3es.Framework.Domain.Persistence;
using d3es.Sales.CommandStack.Commands.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.CommandHandlers
{
    public class CustomerHandlers : ICommandHandler<CreateCustomer>
    {
        private ISession _session;

        public CustomerHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateCustomer message)
        {
            var customer = new Customer(message.FirstName, message.LastName);
            _session.Add<Customer>(customer);

            _session.Commit();
        }
    }
}
