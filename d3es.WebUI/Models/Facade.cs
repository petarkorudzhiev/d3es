using d3es.Framework.Projection;
using d3es.Sales.QueryStack.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models
{
    public class Facade : IFacade
    {
        private IDocumentStore _store;

        public Facade(IDocumentStore store)
        {
            _store = store;
        }

        public CustomersView GetCustomersView()
        {
            var reader = _store.GetReader<string, CustomersView>();
            CustomersView view = null;
            if (!reader.TryGet("customers", out view))
                view = new CustomersView();

            return view;
        }
        public ProductsView GetProductsView()
        {
            var reader = _store.GetReader<string, ProductsView>();
            ProductsView view = null;
            if (!reader.TryGet("products", out view))
                view = new ProductsView();

            return view;
        }
        public CartView GetCartView(Guid customerId)
        {
            var reader = _store.GetReader<Guid, CartView>();
            CartView view = null;
            if (!reader.TryGet(customerId, out view))
                view = new CartView();

            return view;            
        }
    }
}