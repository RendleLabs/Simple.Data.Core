using System;
using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Container : IEquatable<Container>
    {
        public bool Equals(Container other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(ParentContainer, other.ParentContainer) && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Container) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ParentContainer != null ? ParentContainer.GetHashCode() : 0)*397) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
            }
        }

        public static bool operator ==(Container left, Container right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Container left, Container right)
        {
            return !Equals(left, right);
        }

        public Container ParentContainer { get; }
        public string Name { get; }

        public Container(string name) : this(name, null)
        {
        }

        public Container(string name, Container parentContainer)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            ParentContainer = parentContainer;
        }

        public LinkedList<string> QualifiedName => (ParentContainer?.QualifiedName).Add(Name);
    }
}