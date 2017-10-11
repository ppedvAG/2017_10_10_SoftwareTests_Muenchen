using FakeItEasy;
using Moq;
using NSubstitute;
using Pazifiksegler.Core.Models;
using Pazifiksegler.Core.Repositories;
using Pazifiksegler.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Xunit;

namespace Pazifiksegler.WebApi.Tests.Controllers
{
    public class CustomerControllerTests
    {
        [Fact]
        public void Contructor_customerRepository_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerController(null));
        }
        [Fact]
        public void CanCreateInstance()
        {
            var repository = new TestRepository();
            var controller = new CustomerController(repository);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CanCreateInstance_FakeItEasy()
        {
            var repository = A.Fake<ICustomerRepository>();
            var controller = new CustomerController(repository);
            Assert.NotNull(controller);
        }
        [Fact]
        public async void GetAll_retuns_5_customers()
        {
            var repository = new TestRepository();
            var controller = new CustomerController(repository)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();

            var result = await controller.GetCustomers().ExecuteAsync(CancellationToken.None);

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var customers = await result.Content.ReadAsAsync<IEnumerable<Customer>>();
            Assert.Equal(5, customers.Count());
        }
        [Fact]
        public async void GetAll_retuns_5_customers_moq()
        {
            var repositoryMock = new Mock<ICustomerRepository>();
            repositoryMock.Setup(r => r.GetAll()).Returns(new[]
            {
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer()
            });
            var controller = new CustomerController(repositoryMock.Object)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();

            var result = await controller.GetCustomers().ExecuteAsync(CancellationToken.None);

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var customers = await result.Content.ReadAsAsync<IEnumerable<Customer>>();
            Assert.Equal(5, customers.Count());
            repositoryMock.Verify(r => r.GetAll());
        }
        [Fact]
        public async void GetAll_retuns_5_customers_NSubstitute()
        {
            var repository = Substitute.For<ICustomerRepository>();
            repository.GetAll().Returns(new[]
                {
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer()
                });
            var controller = new CustomerController(repository)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();

            var result = await controller.GetCustomers().ExecuteAsync(CancellationToken.None);

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var customers = await result.Content.ReadAsAsync<IEnumerable<Customer>>();
            Assert.Equal(5, customers.Count());
            repository.Received().GetAll();
        }
        [Fact]
        public async void GetAll_retuns_5_customers_FakeItEasy()
        {
            var repository = A.Fake<ICustomerRepository>();
            A.CallTo(() => repository.GetAll()).Returns(new[]
                {
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer()
                });

            var controller = new CustomerController(repository)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();

            var result = await controller.GetCustomers().ExecuteAsync(CancellationToken.None);

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var customers = await result.Content.ReadAsAsync<IEnumerable<Customer>>();
            Assert.Equal(5, customers.Count());
            A.CallTo(() => repository.GetAll()).MustHaveHappened();
        }
        [Fact]
        public async void CreateCustomer_adds_new_customer_to_repository_Statuscode_200()
        {
            var repository = A.Fake<ICustomerRepository>();
            var controller = new CustomerController(repository)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();
            var customerToSave = new Customer
            {
                Id = 0,
                Firstname = "Stanislaus",
                Lastname = "Petermaier"
            };

            var result = await controller.CreateCustomer(customerToSave).ExecuteAsync(CancellationToken.None);

            A.CallTo(() => repository.Add(customerToSave)).MustHaveHappened();

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async void CreateCustomer_null_returns_BadRequest()
        {
            var repository = A.Fake<ICustomerRepository>();
            var controller = new CustomerController(repository)
            {
                Request = new HttpRequestMessage()
            };

            var result = await controller.CreateCustomer(null).ExecuteAsync(CancellationToken.None);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        private class TestRepository : ICustomerRepository
        {
            public void Add(Customer customer) => throw new NotImplementedException();
            public IEnumerable<Customer> GetAll() => new[]
                {
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer(),
                    new Customer()
                };
        }
    }
}
