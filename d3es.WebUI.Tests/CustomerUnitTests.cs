using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using d3es.WebUI.Config;
using d3es.WebUI.Controllers;
using d3es.WebUI.Models.Customer;
using System.Web.Mvc;
using d3es.Sales.QueryStack.Dtos;

namespace d3es.WebUI.Tests
{
    [TestClass]
    public class CustomerUnitTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.Initialize();
        }

        [TestMethod]
        public void CreateCustomer()
        {
            var controller = _container.GetInstance<CustomerController>();
            var model = new CreateCustomerViewModel()
            {
                FirstName = "Petar",
                LastName = "Korudzhiev"
            };

            var result = controller.Create(model) as RedirectToRouteResult;
            object view;
            result.RouteValues.TryGetValue("action", out view);
            Assert.AreEqual("Index", view);

            var viewResult = controller.Index() as ViewResult;

            var viewModel = viewResult.Model as CustomersView;
            Assert.AreEqual(1, viewModel.Customers.Count);
        }
    }
}
