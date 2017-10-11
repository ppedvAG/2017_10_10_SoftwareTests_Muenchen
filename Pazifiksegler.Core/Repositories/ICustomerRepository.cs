using Pazifiksegler.Core.Models;
using System.Collections.Generic;

namespace Pazifiksegler.Core.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        void Add(Customer customer);
    }
}
