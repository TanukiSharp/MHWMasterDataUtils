using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Exporter
{
    public class ConsoleLogger : ILogger
    {
        public string Category { get; }
        public LogLevel MinimumLogLevel { get; }

        public ConsoleLogger(string category, LogLevel minimumLogLevel)
        {
            Category = category;
            MinimumLogLevel = minimumLogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= MinimumLogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel) == false)
                return;

            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [{logLevel}] {(Category != null ? $"[{Category}] " : "")}{formatter(state, exception)}");
        }
    }
}
