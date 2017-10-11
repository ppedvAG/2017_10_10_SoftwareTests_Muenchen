using System.Collections.Generic;
using DecoratorAndDI.Core;
using System;

namespace DecoratorAndDI.Caching
{
    internal class CachingRepository : IRepository
    {
        private readonly IRepository baseRepository;
        private readonly ICache cache;
        private readonly IDateTimeService dateTime;
        private DateTime lastLoadedTime;

        public CachingRepository(IRepository baseRepository, ICache cache, IDateTimeService dateTime)
        {
            this.baseRepository = baseRepository;
            this.cache = cache;
            this.dateTime = dateTime;
        }

        public IEnumerable<string> GetCustomers()
        {
            const string cacheKey = "CachingRepository_GetCustomers";

            var shouldReloadData = (dateTime.Now - lastLoadedTime).TotalSeconds >= 10;

            if (!cache.Contains(cacheKey) || shouldReloadData)
            {
                cache.Add(cacheKey, baseRepository.GetCustomers());
                lastLoadedTime = dateTime.Now;
            }

            return cache.Get<IEnumerable<string>>(cacheKey);
        }
    }
}
