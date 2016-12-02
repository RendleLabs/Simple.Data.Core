using System.Collections.Immutable;
using System.Linq;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Sql.Update
{
    public class UpdateStatement
    {
        public Table Table { get; }
        public string[] Columns { get; }
        public Parameter[] Values { get; }

        private UpdateStatement(Table table, string[] columns, Parameter[] values)
        {
            Table = table;
            Columns = columns;
            Values = values;
        }

        public static UpdateStatement Create(Table table, ImmutableDictionary<string, object> data, DataContext context)
        {
            var parameters = data.Select(kvp => new Parameter(kvp.Key, kvp.Value)).ToArray();
            return new UpdateStatement(table, data.Keys.ToArray(), parameters);
        }
    }
}
