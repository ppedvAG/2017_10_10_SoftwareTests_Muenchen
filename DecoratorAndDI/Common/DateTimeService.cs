using System;
using DecoratorAndDI.Core;

namespace DecoratorAndDI.Common
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
