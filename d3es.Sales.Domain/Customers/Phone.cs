using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers
{
    public class Phone : ValueObject<Phone>
    {
        private string _phone;

        public Phone(string phone)
        {
            Validate(phone);

            _phone = phone;
        }

        private void Validate(string phone)
        {
            if (String.IsNullOrEmpty(phone))
                throw new ArgumentNullException("phone");

            // Additional validation based on phone format
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { _phone };
        }
        public override string ToString()
        {
            return _phone;
        }
    }
}
