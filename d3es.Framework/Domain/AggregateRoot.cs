using d3es.Framework.Events;
using d3es.Framework.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionMagic;
using d3es.Framework.EventStore.Snapshots;

namespace d3es.Framework.Domain
{
    public abstract class AggregateRoot : Entity, IamEventSourced, IOrginator
    {
        private List<IEvent> _uncommittedEvents;
        private int _version;

        protected AggregateRoot()
        {
            _uncommittedEvents = new List<IEvent>();
            _version = 0;
        }

        public IEnumerable<IEvent> UncommittedEvents
        {
            get { return _uncommittedEvents; }
        }
        public int Version
        {
            get { return _version; }
            protected set { _version = value; }
        }

        protected void Apply(IEvent @event)
        {
            Mutate(@event);
            _version++;
            _uncommittedEvents.Add(@event);
        }
        protected abstract IMemento CreateMemento();
        protected abstract void SetMemento(IMemento memento);

        void IamEventSourced.MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }
        void IamEventSourced.LoadHistory(IEnumerable<IEvent> events, int version)
        {
            _version = version;
            foreach (IEvent @event in events)
            {
                Mutate(@event);
            }
        }
        IMemento IOrginator.CreateMemento()
        {
            return CreateMemento();
        }
        void IOrginator.SetMemento(IMemento memento)
        {
            SetMemento(memento);
        }

        private void Mutate(IEvent e)
        {
            this.AsDynamic().When((dynamic)e);
        }
    }
}
