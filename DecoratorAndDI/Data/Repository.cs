using DecoratorAndDI.Core;
using System.Collections.Generic;

namespace DecoratorAndDI.Data
{
    internal class Repository : IRepository
    {
        public IEnumerable<string> GetCustomers()
        {
            // select * from Customers
            return new[]
            {
                "Hans",
                "Peter",
                "Stanislaus"
            };
        }
    }
}
