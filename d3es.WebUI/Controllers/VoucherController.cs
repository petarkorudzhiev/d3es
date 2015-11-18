using d3es.Framework.Bus;
using d3es.Sales.CommandStack.Commands.Voucher;
using d3es.WebUI.Models;
using d3es.WebUI.Models.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3es.WebUI.Controllers
{
    public class VoucherController : BaseController
    {
        public VoucherController(IBus bus, IFacade facade) : base(bus, facade) { }

        // GET: Voucher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateVoucherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateVoucher(model.CustomerId, model.Amount);
            Bus.Send<CreateVoucher>(command);

            return RedirectToAction("Index");
        }
    }
}