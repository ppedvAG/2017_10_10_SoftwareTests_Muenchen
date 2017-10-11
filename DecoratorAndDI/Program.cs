using DecoratorAndDI.Caching;
using DecoratorAndDI.Common;
using DecoratorAndDI.Core;
using DecoratorAndDI.Data;
using DecoratorAndDI.Logging;
using StructureMap;
using System;
using System.Collections.Generic;

namespace DecoratorAndDI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Unit Container
            // MEF - Managed Extensibility Framework

            // NInject http://www.ninject.org/
            // Autofac https://autofac.org/
            // Castle http://www.castleproject.org/projects/windsor/
            // Structuremap http://structuremap.github.io/


            //var logger = new ConsoleLogger();
            //var cache = new SimpleCache();
            //var dateTimeService = new DateTimeService();

            //var repository = new Repository();
            //var loggingRepository = new LoggingRepository(repository, logger, dateTimeService);
            //var cachingRepository = new CachingRepository(loggingRepository, cache, dateTimeService);

            //var controller = new CustomerController(cachingRepository);


            var container = new Container(c =>
            {
                c.For<ILogger>().Use<ConsoleLogger>().Singleton();
                c.For<ICache>().Use<SimpleCache>().Singleton();
                c.For<IDateTimeService>().Use<DateTimeService>().Singleton();

                c.For<IRepository>().Use<Repository>();
                c.For<IRepository>().DecorateAllWith<LoggingRepository>();
                c.For<IRepository>().DecorateAllWith<CachingRepository>();
            });

            var controller = container.GetInstance<CustomerController>();

            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                var customers = controller.GetAll();
                foreach (var c in customers)
                    Console.WriteLine(c);

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }

    internal class CustomerController
    {
        private readonly IRepository repository;

        public CustomerController(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<string> GetAll()
        {
            return repository.GetCustomers();
        }
    }
}
