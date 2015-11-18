using d3es.Framework.Events;
using d3es.Framework.Projection;
using d3es.Sales.Domain.Carts.Events;
using d3es.Sales.QueryStack.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Projections
{
    public class CartViewProjections : IEventHandler<CartCreated>,
                                       IEventHandler<ProductAddedToCart>,
                                       IEventHandler<ProductRemovedFromCart>,
                                       IEventHandler<VoucherAppliedToCart>,
                                       IEventHandler<CartCheckedOut>
    {
        private readonly IDocumentWriter<Guid, CartView> _writer;

        public CartViewProjections(IDocumentStore store)
        {
            _writer = store.GetWriter<Guid, CartView>();
        }

        public void Handle(CartCreated message)
        {
            _writer.Add(message.CustomerId, new CartView() { Id = message.Id });
        }
        public void Handle(ProductAddedToCart message)
        {
            _writer.UpdateOrThrow(message.CustomerId, view => {
                view.Items.Add(new CartItemView() 
                { 
                    ProductId = message.ProductId, 
                    Quantity = message.Quantity.Value 
                });
            });
        }
        public void Handle(ProductRemovedFromCart message)
        {
            _writer.UpdateOrThrow(message.CustomerId, view => { 
                var item = view.Items.SingleOrDefault(i => i.ProductId == message.ProductId);
                if (item != null)
                    view.Items.Remove(item);
            });
        }
        public void Handle(VoucherAppliedToCart message)
        {
            _writer.UpdateOrThrow(message.CustomerId, view => {
                view.VoucherId = message.VoucherId;
                view.VoucherAmount = message.Amount.Value;
            });
        }
        public void Handle(CartCheckedOut message)
        {
            _writer.UpdateOrThrow(message.CustomerId, view => {
                view.Items.Clear();
                view.VoucherId = null;
                view.VoucherAmount = null;
            });
        }
    }
}
