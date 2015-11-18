using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Exceptions
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
