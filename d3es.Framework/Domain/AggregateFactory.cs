using d3es.Framework.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain
{
    internal static class AggregateFactory
    {
        public static T CreateAggregateRoot<T>()
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), true);
            }
            catch (MissingMethodException)
            {
                throw new MissingParameterLessConstructorException(typeof(T));
            }
        }
    }
}
