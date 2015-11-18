using d3es.Framework.Commands;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Bus
{
    public interface IBus : ICommandSender, IEventPublisher, IHandlerRegistrar
    {
    }
}
