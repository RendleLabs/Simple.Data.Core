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
            if (ReferenceEquals(list, null))
            {
                list = new LinkedList<T>();
            }
            list.AddLast(value);
            return list;
        }
    }
}
