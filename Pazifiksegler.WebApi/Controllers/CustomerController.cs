using Pazifiksegler.Core.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Pazifiksegler.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        // api/customer
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return new Data.Repositories.SampleCustomerRepository().GetAll();
        }
    }
}
