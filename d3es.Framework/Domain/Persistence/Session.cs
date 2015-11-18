using d3es.Framework.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Persistence
{
    public class Session : ISession
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Guid, AggregateRoot> _trackedAggregates;

        public Session(IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
            _trackedAggregates = new Dictionary<Guid, AggregateRoot>();
        }

        public void Add<T>(T aggregate) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Id))
                _trackedAggregates.Add(aggregate.Id, aggregate);
            else if (_trackedAggregates[aggregate.Id] != aggregate)
                throw new AggregateAlreadyTrackedException(aggregate.Id);
        }

        public T Get<T>(Guid id) where T : AggregateRoot
        {
            if (IsTracked(id))
                return (T)_trackedAggregates[id];

            var aggregate = _repository.Get<T>(id);
            Add(aggregate);

            return aggregate;
        }

        private bool IsTracked(Guid id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public void Commit()
        {
            foreach (var aggregate in _trackedAggregates.Values)
            {
                _repository.Save(aggregate);
            }
            _trackedAggregates.Clear();
        }
    }
}
