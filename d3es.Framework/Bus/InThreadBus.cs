using d3es.Framework.Commands;
using d3es.Framework.Events;
using d3es.Framework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Bus
{
    public class InThreadBus : IBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<IMessage>> handlers;
            if (_routes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("Cannot send to more than one handler");
                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("No handler registered");
            }
        }
        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;
            foreach (var handler in handlers)
                handler(@event);
        }
        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add((x => handler((T)x)));
        }
    }
}
