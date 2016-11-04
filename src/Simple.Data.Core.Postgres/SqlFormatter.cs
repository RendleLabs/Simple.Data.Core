using System.Collections.Generic;
using Simple.Data.Core.Sql.Insert;
using Simple.Data.Core.Sql.Where;
using System.Linq;

namespace Simple.Data.Core.Postgres
{
    public static class SqlFormatter
    {
        public static string FormatWherePart(WherePart wherePart)
        {
            return $"{QuoteDottedIdentifier(wherePart.Column.QualifiedName)} {wherePart.Operator} @{wherePart.Parameter.Name}";
        }

        public static string QuoteDottedIdentifier(IEnumerable<string> name)
        {
            return string.Join(".", name);
        }

        public static string FormatInsert(InsertStatement insert)
        {
            return $@"INSERT INTO {QuoteDottedIdentifier(insert.Table.QualifiedName)} ({QuoteIdentifierList(insert.Columns)}) VALUES ({string.Join(",", insert.Values.Select(p => "@" + p.Name))}) RETURNING *";
        }

        private static string QuoteIdentifierList(string[] identifiers)
        {
            return string.Join(", ", identifiers);
        }
    }
}