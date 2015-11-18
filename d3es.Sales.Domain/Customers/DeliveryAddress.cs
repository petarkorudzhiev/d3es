using d3es.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers
{
    public class DeliveryAddress : ValueObject<DeliveryAddress>
    {
        public static DeliveryAddress Empty = new DeliveryAddress() 
        {
            City = String.Empty,
            Address = String.Empty
        };

        public DeliveryAddress(string city, string address)
        {
            if (String.IsNullOrEmpty(city))
                throw new ArgumentNullException("city");

            if (String.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            Address = address;
            City = city;
        }
        private DeliveryAddress() { }

        public string City { get; private set; }
        public string Address { get; private set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>() { City, Address };
        }
    }
}
