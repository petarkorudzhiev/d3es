using d3es.Framework.Domain;
using d3es.Framework.Domain.Exceptions;
using d3es.Framework.Domain.Persistence;
using d3es.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore.Snapshots
{
    public class SnapshotRepository : IRepository
    {
        private readonly ISnapshotStore _snapshotStore;
        private readonly ISnapshotStrategy _snapshotStrategy;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEventStore _eventStore;

        public SnapshotRepository(ISnapshotStore snapshotStore, ISnapshotStrategy snapshotStrategy, IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _snapshotStore = snapshotStore;
            _snapshotStrategy = snapshotStrategy;
            _eventPublisher = eventPublisher;
            _eventStore = eventStore;
        }

        public void Save<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : AggregateRoot
        {
            int version = aggregate.Version;
            while(true)
            { 
                try
                {
                    _eventStore.AppendToStream(aggregate.Id, version, aggregate.UncommittedEvents);
                    break;
                }
                catch(EventStoreConcurrencyException ex)
                {
                    foreach (var failedEvent in aggregate.UncommittedEvents) 
                    { 
                        foreach (var succeededEvent in ex.StoreEvents) 
                        { 
                            if (ConflictsWith(failedEvent, succeededEvent)) 
                            { 
                                var msg = string.Format(" Conflict between {0} and {1}", failedEvent, succeededEvent); 
                                throw new ConcurrencyException(msg, ex); 
                            } 
                        } 
                    }

                    // there are no conflicts and we can try append again 
                    version = ex.StoreVersion + 1;
                }
            }

            TryMakeSnapshot(aggregate);

            foreach (IEvent @event in aggregate.UncommittedEvents)
            {
                _eventPublisher.Publish(@event);
            }

            IamEventSourced eventSourced = aggregate as IamEventSourced;
            if (eventSourced == null)
                throw new AggregateNotEventSourcedException(aggregate.Id);

            eventSourced.MarkChangesAsCommitted();
        }
        public TAggregateRoot Get<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            var aggregate = AggregateFactory.CreateAggregateRoot<TAggregateRoot>();
            IamEventSourced eventSourced = aggregate as IamEventSourced;
            if (eventSourced == null)
                throw new AggregateNotEventSourcedException(id);

            EventStream stream = null;
            if (TryRestoreAggregateFromSnapshot(id, aggregate))
                stream = _eventStore.LoadEventStream(id, aggregate.Version, Int32.MaxValue);
            else
                stream = _eventStore.LoadEventStream(id);

            eventSourced.LoadHistory(stream.Events, stream.Version);

            return aggregate;
        }

        private bool TryRestoreAggregateFromSnapshot<TAggregateRoot>(Guid id, TAggregateRoot aggregate)
            where TAggregateRoot : AggregateRoot
        {
            var snapshot = _snapshotStore.Get(id);
            if (snapshot != null)
            {
                IOrginator orginator = aggregate as IOrginator;
                if (orginator != null)
                {
                    orginator.SetMemento(snapshot.State);
                    return true;
                }
            }
            return false;
        }
        private void TryMakeSnapshot(AggregateRoot aggregate)
        {
            if (!_snapshotStrategy.ShouldMakeSnapShot(aggregate))
                return;

            IOrginator orginator = aggregate as IOrginator;
            if (orginator != null)
            {
                var memento = orginator.CreateMemento();
                var snapshot = new Snapshot()
                {
                    AggregateId = aggregate.Id,
                    Version = aggregate.Version,
                    State = memento
                };

                _snapshotStore.Save(snapshot);
            }
        }

        private bool ConflictsWith( IEvent event1, IEvent event2) 
        { 
            return event1. GetType() == event2.GetType(); 
        }
    }
}
