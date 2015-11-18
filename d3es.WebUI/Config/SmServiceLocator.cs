using d3es.Framework.Config;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3es.WebUI.Config
{
    public class SmServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;

        public SmServiceLocator(IContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                         ? _container.TryGetInstance(serviceType)
                         : _container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }
    }
}