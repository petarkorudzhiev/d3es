using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using d3es.WebUI.Config;
using StructureMap;
using d3es.WebUI.Controllers;
using d3es.WebUI.Models.Product;
using System.Web.Mvc;
using d3es.Sales.QueryStack.Dtos;

namespace d3es.WebUI.Tests
{
    [TestClass]
    public class ProductUnitTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.Initialize();
        }

        [TestMethod]
        public void CreateProduct()
        {
            var controller = _container.GetInstance<ProductController>();
            var model = new CreateProductViewModel()
            {
                Name = "Product 1",
                Price = 12.3m
            };

            var result = controller.Create(model) as RedirectToRouteResult;
            object view;
            result.RouteValues.TryGetValue("action", out view);
            Assert.AreEqual("Index", view);

            var viewResult = controller.Index() as ViewResult;

            var viewModel = viewResult.Model as ProductsView;
            Assert.AreEqual(1, viewModel.Products.Count);
        }
    }
}
