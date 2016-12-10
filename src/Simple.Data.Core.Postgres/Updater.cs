using Microsoft.Extensions.Logging;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Update;
using Simple.Data.Core.Sql.Where;

namespace Simple.Data.Core.Postgres
{
    public class Updater : UpdaterBase
    {
        public Updater(string connectionString, ILoggerFactory loggerFactory) : base(connectionString, loggerFactory)
        {
        }
        protected override string FormatSql(UpdateStatement update, WherePart wherePart)
        {
            return SqlFormatter.FormatUpdate(update, wherePart);
        }

        protected override ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters)
        {
            return new PostgresCommandBuilder(connectionString, sql, parameters);
        }
    }
}