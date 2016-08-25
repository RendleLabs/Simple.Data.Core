using System;

namespace Simple.Data.Core
{
    public class Database
    {
        public static dynamic Open(string connectionString, Type adapterType)
        {
            var adapter = (Adapter)Activator.CreateInstance(adapterType, connectionString);
            return Thing.CreateTop(new Wrangler(adapter));
        }
    }
}