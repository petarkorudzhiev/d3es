using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Products
{
    public class Money : ValueObject<Money>
    {
        public static Money Empty { get { return new Money(0.0m); } }

        public Money(decimal value)
        {
            Validate(value);

            Value = value;
        }

        public decimal Value { get; private set; }

        private void Validate(decimal value)
        {
            if (value % 0.01m != 0)
                throw new ArgumentException("More than two decimal places in money value");

            if (value < 0)
                throw new ArgumentException("Money cannot be a negative value");
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { Value };
        }

        public static Money operator +(Money m1, Money m2)
        {
            return new Money(m1.Value + m2.Value);
        }
        public static Money operator -(Money m1, Money m2)
        {
            return new Money(m1.Value - m2.Value);
        }
        public static Money operator *(Money m1, Quantity quantity)
        {
            return new Money(m1.Value * quantity.Value);
        }
        public static Money operator *(Money m1, Money m2)
        {
            return new Money(m1.Value * m2.Value);
        }
        public static bool operator >(Money m1, Money m2)
        {
            return m1.Value > m2.Value;
        }
        public static bool operator <(Money m1, Money m2)
        {
            return m1.Value < m2.Value;
        }
    }
}
