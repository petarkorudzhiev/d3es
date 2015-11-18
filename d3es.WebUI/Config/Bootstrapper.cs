using d3es.Framework.Config;
using d3es.Sales.CommandStack.CommandHandlers;
using d3es.Sales.Domain.Customers;
using d3es.Sales.Domain.Customers.Handlers;
using d3es.Sales.QueryStack.Projections;
using d3es.WebUI.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Config
{
    public static class Bootstrapper
    {
        public static IContainer Initialize()
        {
            var container = IoC.Initialize();
            RegisterHandlers(new SmServiceLocator(container));


            return container;
        }

        private static void RegisterHandlers(IServiceLocator serviceLocator)
        {
            var registrar = new BusRegistrar(serviceLocator);
            registrar.Register(typeof(CartHandlers));
            registrar.Register(typeof(CartViewProjections));
            registrar.Register(typeof(CustomerCreatedEventHandler));
        }
    }
}