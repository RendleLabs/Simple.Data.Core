using System.Collections.Generic;

namespace Simple.Data.Core.Expressions
{
    public class Column
    {
        public string Name { get; }
        public Table Table { get; }

        public Column(string name, Table table)
        {
            Name = name;
            Table = table;
        }

        public LinkedList<string> QualifiedName => Table.QualifiedName.Add(Name);
    }
}