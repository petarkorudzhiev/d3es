using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Dtos
{
    [Serializable]
    public class CartView
    {
        public CartView()
        {
            Items = new List<CartItemView>();
            VoucherId = null;
            VoucherAmount = null;
        }
        public List<CartItemView> Items { get; private set; }

        public Guid Id { get; set; }
        public Guid? VoucherId { get; set; }
        public decimal? VoucherAmount { get; set; }
    }
    [Serializable]
    public class CartItemView
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
