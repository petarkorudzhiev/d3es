using d3es.Framework.Bus;
using d3es.Sales.CommandStack.Commands.Product;
using d3es.WebUI.Models;
using d3es.WebUI.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3es.WebUI.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IBus bus, IFacade facade) : base(bus, facade) { }

        // GET: Product
        public ActionResult Index()
        {
            var model = Facade.GetProductsView();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateProduct(model.Name, model.Price);
            Bus.Send<CreateProduct>(command);

            return RedirectToAction("Index");
        }
    }
}