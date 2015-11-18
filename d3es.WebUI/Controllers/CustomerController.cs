using d3es.Framework.Bus;
using d3es.Sales.CommandStack.Commands.Customer;
using d3es.WebUI.Models;
using d3es.WebUI.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3es.WebUI.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IBus bus, IFacade facade) : base(bus, facade) { }

        // GET: Customer
        public ActionResult Index()
        {
            var model = Facade.GetCustomersView();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateCustomer(model.FirstName, model.LastName);
            Bus.Send<CreateCustomer>(command);

            return RedirectToAction("Index");
        }
    }
}