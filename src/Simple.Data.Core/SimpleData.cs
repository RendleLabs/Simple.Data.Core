using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Logging;
using System.Linq;

namespace Simple.Data.Core
{
    public class SimpleData : ISimpleData
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly SimpleDataOptions _options;

        public SimpleData(ILoggerFactory loggerFactory = null)
        {
            _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
        }

        public SimpleData(SimpleDataOptions options)
        {
            _options = options;
        }

        public dynamic Open(string connectionString, Type adapterType)
        {
            var adapter = (Adapter)Activator.CreateInstance(adapterType, connectionString, _loggerFactory);
            return Thing.CreateTop(new Wrangler(adapter));
        }

        public dynamic Open(string connectionString)
        {
            if (_options?.DefaultAdapterType == null)
            {
                throw new InvalidOperationException("No DefaultAdapterType is specified.");
            }
            return Open(connectionString, _options.DefaultAdapterType);
        }
    }
}