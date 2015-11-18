using d3es.Framework.Domain;
using d3es.Sales.Domain.Customers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers
{
    public class Email : ValueObject<Email>
    {
        private string _email;

        public Email(string email)
        {
            Validate(email);

            _email = email;
        }

        private void Validate(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new InvalidEmailException();

            var result = Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (!result)
                throw new InvalidEmailException();
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { _email };
        }
        public override string ToString()
        {
            return _email;
        }
    }
}
