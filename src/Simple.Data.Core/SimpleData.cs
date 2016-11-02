using System;

namespace Simple.Data.Core
{
    public class SimpleData : ISimpleData
    {
        private readonly SimpleDataOptions _options;

        public SimpleData()
        {
            
        }

        public SimpleData(SimpleDataOptions options)
        {
            _options = options;
        }

        public dynamic Open(string connectionString, Type adapterType)
        {
            var adapter = (Adapter)Activator.CreateInstance(adapterType, connectionString);
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