using System;

namespace Simple.Data.Core
{
    public interface ISimpleData
    {
        dynamic Open(string connectionString, Type adapterType);
    }
}