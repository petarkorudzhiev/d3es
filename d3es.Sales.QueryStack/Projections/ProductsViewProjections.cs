using d3es.Framework.Events;
using d3es.Framework.Projection;
using d3es.Sales.Domain.Products.Events;
using d3es.Sales.QueryStack.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Projections
{
    public class ProductsViewProjections : IEventHandler<ProductCreated>
    {
        private readonly IDocumentWriter<string, ProductsView> _writer;

        public ProductsViewProjections(IDocumentStore store)
        {
            _writer = store.GetWriter<string, ProductsView>();
        }

        public void Handle(ProductCreated message)
        {
            var product = new ProductView()
            {
                Id = message.Id,
                Name = message.Name,
                Price = message.Price.Value
            };

            var products = new ProductsView();
            products.Products.Add(product);

            _writer.AddOrUpdate("products", products, view =>
            {
                view.Products.Add(product);
            }); 
        }
    }
}
