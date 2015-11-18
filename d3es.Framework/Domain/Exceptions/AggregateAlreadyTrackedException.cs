using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Exceptions
{
    public class AggregateAlreadyTrackedException : Exception
    {
        public AggregateAlreadyTrackedException(Guid id) :
            base(string.Format("Aggregate with Id: {0} is already tracked in session", id)) { }
    }
}
