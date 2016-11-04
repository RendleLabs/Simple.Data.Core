using System;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Logging
{
    internal struct NullLoggerFactory : ILoggerFactory
    {
        public static readonly ILoggerFactory Instance = new NullLoggerFactory();
        private static readonly ILogger Null = new NullLogger();
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return Null;
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        private struct NullLogger : ILogger
        {
            private static readonly IDisposable Null = new NullDisposable();

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return Null;
            }

            private struct NullDisposable : IDisposable
            {
                public void Dispose()
                {
                }
            }
        }
    }
}