using System.Collections.Generic;

namespace DecoratorAndDI.Core
{
    internal interface IRepository
    {
        IEnumerable<string> GetCustomers();
    }
}
