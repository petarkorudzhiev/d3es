using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain.Exceptions
{
    public class MissingParameterLessConstructorException : System.Exception
    {
        public MissingParameterLessConstructorException(Type type)
            : base(string.Format("{0} has no constructor without paramerters. This can be either public or private", type.FullName))
        {
        }
    }
}
