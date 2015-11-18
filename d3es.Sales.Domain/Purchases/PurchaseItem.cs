using d3es.Sales.Domain.Products;
using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Purchases
{
    public class PurchaseItem : ValueObject<PurchaseItem>
    {
        public PurchaseItem(Guid productId, Money price, Quantity quantity)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public Guid ProductId { get; private set; }
        public Money Price { get; private set; }
        public Quantity Quantity { get; private set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { ProductId, Price, Quantity };
        }
    }
}
