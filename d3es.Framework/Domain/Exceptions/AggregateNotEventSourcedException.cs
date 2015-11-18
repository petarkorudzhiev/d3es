using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Exceptions
{
    public class AggregateNotEventSourcedException : System.Exception
    {
        public AggregateNotEventSourcedException(Guid id)
            : base(string.Format("Aggregate {0} was not found", id))
        {
        }
    }
}
