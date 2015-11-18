using d3es.Framework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Bus
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
    }
}
