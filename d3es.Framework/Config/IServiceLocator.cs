using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Config
{
    public interface IServiceLocator
    {
        T GetService<T>();
        object GetService(Type type);
    }
}
