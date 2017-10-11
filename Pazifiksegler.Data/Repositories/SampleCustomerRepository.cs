using Pazifiksegler.Core.Models;
using Pazifiksegler.Core.Repositories;
using System.Collections.Generic;
using Tynamix.ObjectFiller;

namespace Pazifiksegler.Data.Repositories
{
    public class SampleCustomerRepository : ICustomerRepository
    {
        public void Add(Customer customer)
        { /* no need to save in SampleRepository */ }

        public IEnumerable<Customer> GetAll() 
        {
            var customerFiller = new Filler<Customer>();
            customerFiller.Setup()
                .OnProperty(c => c.Firstname).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(c => c.Lastname).Use(new RealNames(NameStyle.LastName))
                .OnProperty(c => c.Email).Use(new EmailAddresses(".de"));

            return customerFiller.Create(50);
        }
    }
}
