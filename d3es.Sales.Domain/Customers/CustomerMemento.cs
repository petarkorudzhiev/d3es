using d3es.Framework.EventStore.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers
{
    [Serializable]
    public class CustomerMemento : IMemento
    {
        public CustomerMemento(Guid id, int version, string firstName, string lastName, string email, string phone, string city, string address)
        {
            Id = id;
            Version = version;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            City = city;
            Address = address;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
    }
}
