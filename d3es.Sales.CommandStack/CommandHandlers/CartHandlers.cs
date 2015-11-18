using d3es.Sales.Domain.Carts;
using d3es.Sales.Domain.Customers;
using d3es.Sales.Domain.Products;
using d3es.Sales.Domain.Purchases;
using d3es.Sales.Domain.Vouchers;
using d3es.Framework.Commands;
using d3es.Framework.Domain.Persistence;
using d3es.Sales.CommandStack.Commands.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.CommandStack.CommandHandlers
{
    public class CartHandlers : ICommandHandler<AddProductToCart>,
                                ICommandHandler<RemoveProductFromCart>,
                                ICommandHandler<ApplyVoucherToCart>,
                                ICommandHandler<CheckoutCart>
    {
        private ISession _session;

        public CartHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(AddProductToCart message)
        {
            var cart = _session.Get<Cart>(message.CartId);
            var product = _session.Get<Product>(message.ProductId);

            cart.AddProduct(product, new Quantity(message.Quantity));

            _session.Add<Cart>(cart);
            _session.Commit();
        }
        public void Handle(RemoveProductFromCart message)
        {
            var cart = _session.Get<Cart>(message.CustomerId);
            var product = _session.Get<Product>(message.ProductId);

            cart.RemoveProduct(product);

            _session.Add<Cart>(cart);

            _session.Commit();
        }
        public void Handle(ApplyVoucherToCart message)
        {
            var cart = _session.Get<Cart>(message.CustomerId);
            var voucher = _session.Get<Voucher>(message.VoucherId);

            cart.ApplyVoucher(voucher);

            _session.Add<Cart>(cart);

            _session.Commit();
        }
        public void Handle(CheckoutCart message)
        {
            var cart = _session.Get<Cart>(message.CartId);

            DeliveryAddress address = new DeliveryAddress(message.City, message.Address);
            Phone phone = new Phone(message.Phone);

            var purchase = cart.Checkout(address, phone);

            _session.Add<Purchase>(purchase);
            _session.Add<Cart>(cart);

            _session.Commit();
        }
    }
}
