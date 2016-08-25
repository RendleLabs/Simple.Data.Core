using System.Collections.Generic;

namespace Simple.Data.Core.SqlServer
{
    internal static class QuoteHelper
    {
        public static string Quote(LinkedList<string> name)
        {
            return "[" + string.Join("].[", name) + "]";
        }
    }
}