using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
