using System;
using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Column : IEquatable<Column>
    {
        public bool Equals(Column other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && Table.Equals(other.Table);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Column) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StringComparer.OrdinalIgnoreCase.GetHashCode(Name)*397) ^ Table.GetHashCode();
            }
        }

        public static bool operator ==(Column left, Column right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Column left, Column right)
        {
            return !Equals(left, right);
        }

        public string Name { get; }
        public Table Table { get; }

        public Column(string name, Table table)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (table == null) throw new ArgumentNullException(nameof(table));
            Name = name;
            Table = table;
        }

        public LinkedList<string> QualifiedName => Table.QualifiedName.Add(Name);
    }
}