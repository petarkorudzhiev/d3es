using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.QueryStack.Dtos
{
    [Serializable]
    public class CustomersView
    {
        public List<CustomerView> Customers { get; private set; }

        public CustomersView()
        {
            Customers = new List<CustomerView>();
        }
    }
    [Serializable]
    public class CustomerView
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
