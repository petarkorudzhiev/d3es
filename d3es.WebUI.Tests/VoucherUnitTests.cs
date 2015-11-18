using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using d3es.WebUI.Config;
using d3es.WebUI.Controllers;
using d3es.WebUI.Models.Voucher;
using System.Web.Mvc;

namespace d3es.WebUI.Tests
{
    [TestClass]
    public class VoucherUnitTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.Initialize();
        }
        
        [TestMethod]
        public void CreateVoucher()
        {
            var controller = _container.GetInstance<VoucherController>();
            var model = new CreateVoucherViewModel()
            {
                CustomerId = Guid.NewGuid(),
                Amount = 10m
            };

            var result = controller.Create(model) as RedirectToRouteResult;
            object view;
            result.RouteValues.TryGetValue("action", out view);
            Assert.AreEqual("Index", view);
        }
    }
}
