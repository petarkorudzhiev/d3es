using d3es.Framework.Domain.Exceptions;
using d3es.Framework.Events;
using d3es.Framework.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Persistence
{
    public class Repository : IRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _eventPublisher;

        public Repository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");
            if (eventPublisher == null)
                throw new ArgumentNullException("publisher");

            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public void Save<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : AggregateRoot
        {
            _eventStore.AppendToStream(aggregate.Id, aggregate.Version, aggregate.UncommittedEvents);

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
            var aggregateRoot = AggregateFactory.CreateAggregateRoot<TAggregateRoot>();

            var stream = _eventStore.LoadEventStream(id);

            IamEventSourced eventSourced = aggregateRoot as IamEventSourced;
            if (eventSourced == null)
                throw new AggregateNotEventSourcedException(id);

            eventSourced.LoadHistory(stream.Events, stream.Version);

            return aggregateRoot;
        }
    }
}
