using System;
using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Table : IEquatable<Table>
    {
        public bool Equals(Table other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && Container.Equals(other.Container);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Table) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StringComparer.OrdinalIgnoreCase.GetHashCode(Name)*397) ^ Container.GetHashCode();
            }
        }

        public static bool operator ==(Table left, Table right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Table left, Table right)
        {
            return !Equals(left, right);
        }

        public Table(string name, Container container)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (container == null) throw new ArgumentNullException(nameof(container));
            Name = name;
            Container = container;
        }

        public string Name { get; }

        public Container Container { get; }

        public LinkedList<string> QualifiedName => (Container?.QualifiedName).Add(Name);
    }
}