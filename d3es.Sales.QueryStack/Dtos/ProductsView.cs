using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Dtos
{
    [Serializable]
    public class ProductsView
    {
        public List<ProductView> Products { get; private set; }

        public ProductsView()
        {
            Products = new List<ProductView>();
        }
    }

    [Serializable]
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
