using d3es.Framework.Domain;
using d3es.Sales.Domain.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Products
{
    public class Quantity : ValueObject<Quantity>
    {
        private int _value;

        public int Value 
        {
            get { return _value; }
        }

        public Quantity(int value)
        {
            if (value <= 0)
                throw new InvalidQuantityException();

            _value = value;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { _value };
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.Value + q2.Value);
        }
        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.Value - q2.Value);
        }
        public static bool operator >(Quantity q1, Quantity q2)
        {
            return q1.Value > q2.Value;
        }
        public static bool operator <(Quantity q1, Quantity q2)
        {
            return q1.Value < q2.Value;
        }
    }
}
