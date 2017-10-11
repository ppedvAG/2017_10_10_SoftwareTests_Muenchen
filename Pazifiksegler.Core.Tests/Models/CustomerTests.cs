using Pazifiksegler.Core.Models;
using Xunit;

namespace Pazifiksegler.Core.Tests.Models
{
    public class CustomerTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var customer = new Customer();
            Assert.NotNull(customer);
            Assert.Equal(0, customer.Id);
            Assert.Null(customer.Firstname);
            Assert.Null(customer.Lastname);
            Assert.Null(customer.Email);
        }
    }
}
