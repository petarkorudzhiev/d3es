using d3es.Framework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Events
{
    public interface IEventHandler<T> : IHandler<T> where T : IEvent
    {
    }
}
