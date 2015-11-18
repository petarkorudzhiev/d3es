using d3es.Framework.Domain;
using d3es.Framework.EventStore.Snapshots;
using d3es.Sales.Domain.Customers;
using d3es.Sales.Domain.Customers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public Customer(string firstName, string lastName)
        {
            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentNullException("firstName");

            if (String.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("lastName");

            Apply(new CustomerCreated(Guid.NewGuid(), firstName, lastName));
        }
        protected Customer() { }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }

        public void SetDeliveryAddress(DeliveryAddress address)
        {
            Apply(new CustomerDeliveryAddressChanged(Id, address));
        }
        public void SetEmail(Email email)
        {
            Apply(new CustomerEmailChanged(Id, email));
        }
        public void SetPhone(Phone phone)
        {
            Apply(new CustomerPhoneChanged(Id, phone));
        }

        private void When(CustomerCreated e)
        {
            Id = e.Id;
            FirstName = e.FirstName;
            LastName = e.LastName;
        }
        private void When(CustomerEmailChanged e)
        {
            Email = e.Email;
        }
        private void When(CustomerPhoneChanged e)
        {
            Phone = e.Phone;
        }
        private void When(CustomerDeliveryAddressChanged e)
        {
            DeliveryAddress = e.Address;
        }

        protected override IMemento CreateMemento()
        {
            return new CustomerMemento(Id,
                                       Version,
                                       FirstName,
                                       LastName,
                                       Email != null ? Email.ToString() : null,
                                       Phone != null ? Phone.ToString() : null,
                                       DeliveryAddress != null ? DeliveryAddress.City : null,
                                       DeliveryAddress != null ? DeliveryAddress.Address : null);
        }
        protected override void SetMemento(IMemento memento)
        {
            var customerMemento = (CustomerMemento)memento;

            Id = customerMemento.Id;
            Version = customerMemento.Version;
            FirstName = customerMemento.FirstName;
            LastName = customerMemento.LastName;
            Email = customerMemento.Email != null ? new Email(customerMemento.Email) : null;
            Phone = customerMemento.Phone != null ? new Phone(customerMemento.Phone) : null;

            if (!String.IsNullOrEmpty(customerMemento.City) && !String.IsNullOrEmpty(customerMemento.Address))
                DeliveryAddress = new DeliveryAddress(customerMemento.City, customerMemento.Address);
            else
                DeliveryAddress = null;
        }
    }
}
