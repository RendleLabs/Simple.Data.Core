using System;
using System.Collections.Generic;

namespace Simple.Data.Core
{
    public class SimpleDataOptions
    {
        public Type DefaultAdapterType { get; set; }
        public IDictionary<string,string> ConnectionStrings { get; set; }
    }
}