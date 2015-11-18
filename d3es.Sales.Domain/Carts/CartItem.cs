using d3es.Sales.Domain.Products;
using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Carts
{
    public class CartItem : ValueObject<CartItem>
    {
        public CartItem(Product product, Quantity quantity)
        {
            ProductId = product.Id;
            Quantity = quantity;
            Price = product.Price;
        }
        public CartItem(Guid productId, Quantity quantity, Money price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        protected CartItem() { }

        public Guid ProductId { get; private set; }
        public Quantity Quantity { get; private set; }
        public Money Price { get; private set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { ProductId, Quantity, Price };
        }

        public void AddQuantity(Quantity quantity)
        {
            Quantity += quantity;
        }
    }
}
