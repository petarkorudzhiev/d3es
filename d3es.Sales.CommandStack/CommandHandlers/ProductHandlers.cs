using d3es.Sales.Domain.Products;
using d3es.Framework.Commands;
using d3es.Framework.Domain.Persistence;
using d3es.Sales.CommandStack.Commands.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.CommandHandlers
{
    public class ProductHandlers : ICommandHandler<CreateProduct>
    {
        private ISession _session;

        public ProductHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateProduct message)
        {
            var product = new Product(message.Name, new Money(message.Price));
            _session.Add<Product>(product);

            _session.Commit();
        }
    }
}
