using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Select;
using Simple.Data.Core.Sql.Where;

namespace Simple.Data.Core.Postgres
{
    public class Selecter : SelecterBase
    {
        public Selecter(string connectionString, ILoggerFactory loggerFactory) : base(connectionString, loggerFactory)
        {
        }

        protected override ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters)
        {
            return new PostgresCommandBuilder(connectionString, sql, parameters);
        }

        protected override string FormatSql(QueryCommand command, WherePart wherePart)
        {
            return $"SELECT * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)} LIMIT 1";
        }
    }
}