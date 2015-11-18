using d3es.Sales.QueryStack.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Models
{
    public interface IFacade
    {
        CustomersView GetCustomersView();
        ProductsView GetProductsView();
        CartView GetCartView(Guid customerId);
    }
}