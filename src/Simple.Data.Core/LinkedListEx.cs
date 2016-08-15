using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public static class LinkedListEx
    {
        public static LinkedList<T> Add<T>(this LinkedList<T> list, T value)
        {
            list.AddLast(value);
            return list;
        }
    }
}
