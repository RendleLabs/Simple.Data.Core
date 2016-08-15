using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Container
    {
        public string Name { get; }

        public Container(string name)
        {
            Name = name;
        }

        public LinkedList<string> QualifiedName => new LinkedList<string> {Name};
    }
}