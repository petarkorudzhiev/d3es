using d3es.Framework.Events;
using d3es.Framework.Projection;
using d3es.Sales.Domain.Customers.Events;
using d3es.Sales.QueryStack.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Projections
{
    public class CustomersViewProjections : IEventHandler<CustomerCreated>
    {
        private readonly IDocumentWriter<string, CustomersView> _writer;

        public CustomersViewProjections(IDocumentStore store)
        {
            _writer = store.GetWriter<string, CustomersView>();
        }

        public void Handle(CustomerCreated message)
        {
            var customer = new CustomerView()
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName
            };

            var cView = new CustomersView();
            cView.Customers.Add(customer);

            _writer.AddOrUpdate("customers", cView, view =>
            {
                view.Customers.Add(customer);
            });            
        }
    }
}
