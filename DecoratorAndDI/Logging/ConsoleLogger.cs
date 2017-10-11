using System;

namespace DecoratorAndDI.Logging
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}
