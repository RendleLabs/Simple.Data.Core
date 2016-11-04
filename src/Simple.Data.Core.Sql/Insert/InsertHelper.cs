using System;
using System.Collections.Immutable;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Expressions;
using System.Linq;

namespace Simple.Data.Core.Sql.Insert
{
    public class InsertStatement
    {
        public Table Table { get; }
        public string[] Columns { get; }
        public Parameter[] Values { get; }

        private InsertStatement(Table table, string[] columns, Parameter[] values)
        {
            Table = table;
            Columns = columns;
            Values = values;
        }

        public static InsertStatement Create(Table table, ImmutableDictionary<string, object> data, DataContext context)
        {
            var parameters = data.Select(kvp => new Parameter(kvp.Key, kvp.Value)).ToArray();
            return new InsertStatement(table, data.Keys.ToArray(), parameters);
        }
    }
}