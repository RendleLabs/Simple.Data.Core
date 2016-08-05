using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.Data.Core.Sql
{
    public interface IParameter
    {
        
    }

    public class Parameter<T> : IParameter
    {
        public Parameter(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public T Value { get; }
    }
}
