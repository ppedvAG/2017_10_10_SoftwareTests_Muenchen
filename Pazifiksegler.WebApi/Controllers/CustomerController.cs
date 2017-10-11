using Pazifiksegler.Core.Models;
using Pazifiksegler.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Pazifiksegler.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository) => this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

        // api/customer
        [HttpGet]
        public IHttpActionResult GetCustomers() => Ok(customerRepository.GetAll());

        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customerToSave)
        {
            if (customerToSave == null)
                return BadRequest();

            customerRepository.Add(customerToSave);
            return Ok(customerToSave);
        }
    }
}
