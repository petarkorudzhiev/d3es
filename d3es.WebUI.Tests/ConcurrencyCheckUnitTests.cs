using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using d3es.WebUI.Config;
using d3es.Sales.Domain.Customers;
using d3es.Framework.Domain.Persistence;
using d3es.Framework.Domain.Exceptions;

namespace d3es.WebUI.Tests
{
    [TestClass]
    public class ConcurrencyCheckUnitTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(ConcurrencyException))]
        public void ConcurrencyCheck()
        {
            var customer = new Customer("Petar", "Korudzhiev");

            var repository = _container.GetInstance<IRepository>();

            repository.Save<Customer>(customer);

            var cus1 = repository.Get<Customer>(customer.Id);
            var cus2 = repository.Get<Customer>(customer.Id);

            cus1.SetEmail(new Email("petar.korudzhiev@mentormate.com"));

            cus2.SetEmail(new Email("petar.korudzhiev@mentormate.com"));

            repository.Save<Customer>(cus1);
            //Should throw exception
            repository.Save<Customer>(cus2);
        }
    }
}
