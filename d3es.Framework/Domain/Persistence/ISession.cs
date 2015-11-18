using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Persistence
{
    public interface ISession
    {
        void Add<TAggregateRoot>(TAggregateRoot aggregate) 
            where TAggregateRoot : AggregateRoot;

        TAggregateRoot Get<TAggregateRoot>(Guid id) 
            where TAggregateRoot : AggregateRoot;

        void Commit();
    }
}
