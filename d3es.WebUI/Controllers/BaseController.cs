using d3es.Framework.Bus;
using d3es.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3es.WebUI.Controllers
{
    public class BaseController : Controller
    {
        private IBus _bus;
        private IFacade _facade;

        public BaseController(IBus bus, IFacade facade)
        {
            _bus = bus;
            _facade = facade;
        }

        protected IBus Bus
        {
            get { return _bus; }
        }
        protected IFacade Facade
        {
            get { return _facade; }
        }
    }
}