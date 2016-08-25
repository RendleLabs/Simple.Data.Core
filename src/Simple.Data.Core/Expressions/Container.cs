using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Container
    {
        public Container ParentContainer { get; }
        public string Name { get; }

        public Container(string name) : this(name, null)
        {
        }

        public Container(string name, Container parentContainer)
        {
            Name = name;
            ParentContainer = parentContainer;
        }

        public LinkedList<string> QualifiedName => (ParentContainer?.QualifiedName).Add(Name);
    }
}