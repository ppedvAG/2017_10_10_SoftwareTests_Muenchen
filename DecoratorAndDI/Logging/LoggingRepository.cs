using DecoratorAndDI.Core;
using System.Collections.Generic;
using System.Linq;

namespace DecoratorAndDI.Logging
{
    internal class LoggingRepository : IRepository
    {
        private readonly IRepository baseRepository;
        private readonly ILogger logger;
        private readonly IDateTimeService dateTime;

        public LoggingRepository(IRepository baseRepository, ILogger logger, IDateTimeService dateTime)
        {
            this.baseRepository = baseRepository;
            this.logger = logger;
            this.dateTime = dateTime;
        }

        public IEnumerable<string> GetCustomers()
        {
            logger.Log($"{dateTime.Now.ToString("HH:mm:ss.fff")} | Customers werden geladen.");

            var customers = baseRepository.GetCustomers();

            logger.Log($"{dateTime.Now.ToString("HH:mm:ss.fff")} | {customers.Count()} Customers wurden geladen.");

            return customers;
        }
    }
}
