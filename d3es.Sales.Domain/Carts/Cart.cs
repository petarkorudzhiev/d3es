using d3es.Sales.Domain.Customers;
using d3es.Sales.Domain.Products;
using d3es.Sales.Domain.Purchases;
using d3es.Framework.Domain;
using d3es.Framework.EventStore.Snapshots;
using d3es.Sales.Domain.Carts;
using d3es.Sales.Domain.Carts.Events;
using d3es.Sales.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3es.Sales.Domain.Products.Exceptions;
using d3es.Sales.Domain.Carts.Exceptions;
using d3es.Sales.Domain.Vouchers.Exceptions;

namespace d3es.Sales.Domain.Carts
{
    public class Cart : AggregateRoot
    {
        #region Fields

        private List<CartItem> _items;

        #endregion

        #region Constructors and Factories

        public Cart(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentNullException("customerId");

            Apply(new CartCreated(Guid.NewGuid(), customerId));
        }
        protected Cart() { }

        #endregion

        #region Public State

        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<CartItem> Items 
        {
            get { return _items.AsReadOnly(); } 
        }
        public Money VoucherAmount { get; private set; }
        public Guid? VoucherId { get; private set; }

        #endregion

        #region Public Interface

        public Money GetTotalPrice()
        {
            Money price = Money.Empty;

            foreach (var item in Items)
            {
                price += item.Price * item.Quantity;
            }

            if (VoucherAmount != null && price > VoucherAmount)
            {
                price = price - VoucherAmount;
            }
            return price;
        }
        public void AddProduct(Product product, Quantity quantity)
        {
            if (product == null)
                throw new InvalidProductInstanceException();

            if (quantity == null)
                throw new InvalidQuantityInstanceException();

            Apply(new ProductAddedToCart(CustomerId, product.Id, quantity, product.Price));
        }
        public void RemoveProduct(Product product)
        {
            if (product == null)
                throw new InvalidProductInstanceException();

            if (!_items.Any(i => i.ProductId == product.Id))
                throw new ProductNotFoundInCartException();

            Apply(new ProductRemovedFromCart(CustomerId, product.Id));
        }
        public void ApplyVoucher(Voucher voucher)
        {
            if (voucher == null)
                throw new InvalidVoucherInstanceException();

            Apply(new VoucherAppliedToCart(CustomerId, voucher.Id, voucher.Amount));
        }
        public Purchase Checkout(DeliveryAddress address, Phone phone)
        {
            if (_items.Count == 0)
                throw new CartEmptyException();

            var purchase = new Purchase(this);

            Apply(new CartCheckedOut(CustomerId, address, phone));

            return purchase;
        }

        #endregion

        #region Domain Events Handlers

        private void When(CartCreated e)
        {
            Id = e.Id;
            CustomerId = e.CustomerId;
            VoucherAmount = null;
            VoucherId = null;
            _items = new List<CartItem>();
        }
        private void When(ProductAddedToCart e)
        {
            _items.Add(new CartItem(e.ProductId, e.Quantity, e.Price));
        }
        private void When(ProductRemovedFromCart e)
        {
            var item = _items.SingleOrDefault(p => p.ProductId == e.ProductId);
            _items.Remove(item);
        }
        private void When(VoucherAppliedToCart e)
        {
            VoucherId = e.VoucherId;
            VoucherAmount = e.Amount;
        }
        private void When(CartCheckedOut e)
        {
            _items.Clear();
            VoucherId = null;
            VoucherAmount = null;
        }

        #endregion

        #region Aggregate Memento

        protected override IMemento CreateMemento()
        {
            var items = new List<CartItemMemento>();
            foreach (var item in Items)
            {
                items.Add(new CartItemMemento(item.ProductId, item.Quantity.Value, item.Price.Value));
            }
            return new CartMemento(Id, Version, CustomerId, VoucherAmount != null ? VoucherAmount.Value : (decimal?)null, VoucherId, items);
        }
        protected override void SetMemento(IMemento memento)
        {
            var cartMemento = (CartMemento)memento;

            Id = cartMemento.Id;
            Version = cartMemento.Version;
            CustomerId = cartMemento.CustomerId;
            VoucherAmount = cartMemento.VoucherAmount.HasValue ? new Money(cartMemento.VoucherAmount.Value) : null;
            VoucherId = cartMemento.VoucherId;
            _items = new List<CartItem>();

            foreach (var item in cartMemento.CartItems)
            {
                _items.Add(new CartItem(item.ProductId, new Quantity(item.Quantity), new Money(item.Price)));
            }
        }

        #endregion
    }
}
