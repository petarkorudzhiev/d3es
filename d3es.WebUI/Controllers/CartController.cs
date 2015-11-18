using d3es.Framework.Bus;
using d3es.Sales.CommandStack.Commands.Cart;
using d3es.WebUI.Models;
using d3es.WebUI.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3es.WebUI.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IBus bus, IFacade facade) : base(bus, facade) { }

        // GET: Cart
        public ActionResult Index(Guid customerId)
        {
            var model = Facade.GetCartView(customerId);

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new AddProductToCart(model.CartId, model.ProductId, model.Quantity);
            Bus.Send<AddProductToCart>(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveProduct(RemoveProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new RemoveProductFromCart(model.CustomerId, model.ProductId);
            Bus.Send<RemoveProductFromCart>(command);

            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public ActionResult ApplyVoucher(ApplyVoucherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new ApplyVoucherToCart(model.CustomerId, model.VoucherId);
            Bus.Send<ApplyVoucherToCart>(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CheckoutCart(model.CartId, model.City, model.Address, model.Phone);
            Bus.Send<CheckoutCart>(command);

            return RedirectToAction("Index");
        }
    }
}