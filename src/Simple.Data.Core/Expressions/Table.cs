using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Table
    {
        public Table(string name, Container container = default(Container))
        {
            Name = name;
            Container = container;
        }

        public string Name { get; }

        public Container Container { get; }

        public LinkedList<string> QualifiedName => (Container?.QualifiedName).Add(Name);
    }
}