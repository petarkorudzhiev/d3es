using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using d3es.WebUI.Config;
using d3es.WebUI.Controllers;
using d3es.WebUI.Models.Cart;
using System.Web.Mvc;
using d3es.WebUI.Models.Customer;
using d3es.WebUI.Models.Product;
using d3es.Sales.QueryStack.Dtos;

namespace d3es.WebUI.Tests
{
    [TestClass]
    public class CartUnitTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.Initialize();
        }

        private Guid CreateProduct()
        {
            var controller = _container.GetInstance<ProductController>();
            var model = new CreateProductViewModel()
            {
                Name = "Product 1",
                Price = 12.3m
            };
            var result = controller.Create(model) as RedirectToRouteResult;
            var viewResult = controller.Index() as ViewResult;
            var viewModel = viewResult.Model as ProductsView;

            return viewModel.Products[0].Id;
        }
        private Guid CreateCustomer()
        {
            var controller = _container.GetInstance<CustomerController>();
            var model = new CreateCustomerViewModel()
            {
                FirstName = "Petar",
                LastName = "Korudzhiev"
            };
            var result = controller.Create(model) as RedirectToRouteResult;
            var viewResult = controller.Index() as ViewResult;
            var viewModel = viewResult.Model as CustomersView;

            return viewModel.Customers[0].Id;
        }

        [TestMethod]
        public Guid AddProductToCart()
        {
            var customerId = CreateCustomer();

            var controller = _container.GetInstance<CartController>();

            var viewResult = controller.Index(customerId) as ViewResult;
            var cartModel = viewResult.Model as CartView;

            var model = new AddProductViewModel()
            {
                CartId = cartModel.Id,
                ProductId = CreateProduct(),
                Quantity = 1
            };

            var result = controller.AddProduct(model) as RedirectToRouteResult;
            object view;
            result.RouteValues.TryGetValue("action", out view);
            Assert.AreEqual("Index", view);

            return customerId;
        }

        [TestMethod]
        public void RemoveProductFromCart()
        {

        }

        [TestMethod]
        public void ApplyVoucherToCart()
        {

        }

        [TestMethod]
        public void CheckoutCart()
        {
            var customerId = AddProductToCart();

            var controller = _container.GetInstance<CartController>();

            var viewResult = controller.Index(customerId) as ViewResult;
            var cartModel = viewResult.Model as CartView;

            var model = new CheckoutViewModel()
            {
                CartId = cartModel.Id,
                Phone = "123456",
                City = "Varna",
                Address = "Varna"
            };
            var result = controller.Checkout(model) as RedirectToRouteResult;
            object view;
            result.RouteValues.TryGetValue("action", out view);
            Assert.AreEqual("Index", view);
        }
    }
}
