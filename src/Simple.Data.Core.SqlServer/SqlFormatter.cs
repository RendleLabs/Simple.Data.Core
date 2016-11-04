using System.Collections.Generic;
using System.Linq;
using Simple.Data.Core.Sql.Insert;
using Simple.Data.Core.Sql.Where;

namespace Simple.Data.Core.SqlServer
{
    public static class SqlFormatter
    {
        public static string FormatWherePart(WherePart wherePart)
        {
            return $"{QuoteDottedIdentifier(wherePart.Column.QualifiedName)} {wherePart.Operator} @{wherePart.Parameter.Name}";
        }

        public static string QuoteDottedIdentifier(IEnumerable<string> name)
        {
            return "[" + string.Join("].[", name) + "]";
        }

        public static string QuoteIdentifierList(IEnumerable<string> name)
        {
            return "[" + string.Join("], [", name) + "]";
        }

        public static string FormatInsert(InsertStatement insert)
        {
            return $@"INSERT {QuoteDottedIdentifier(insert.Table.QualifiedName)} ({QuoteIdentifierList(insert.Columns)}) OUTPUT INSERTED.* VALUES ({string.Join(",", insert.Values.Select(p => "@" + p.Name))})";
        }
    }
}