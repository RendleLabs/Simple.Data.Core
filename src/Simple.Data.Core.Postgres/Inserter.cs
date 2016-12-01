using Microsoft.Extensions.Logging;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Insert;

namespace Simple.Data.Core.Postgres
{
    public class Inserter : InserterBase
    {
        public Inserter(string connectionString, ILoggerFactory loggerFactory) : base(connectionString, loggerFactory)
        {
        }

        protected override string FormatSql(InsertStatement insert)
        {
            return SqlFormatter.FormatInsert(insert);
        }

        protected override ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters)
        {
            return new PostgresCommandBuilder(connectionString, sql, parameters);
        }
    }
}