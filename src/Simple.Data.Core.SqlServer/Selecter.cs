using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql.Select;
using Simple.Data.Core.Sql.Where;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.SqlServer
{
    public class Selecter : SelecterBase
    {
        private const string Top1 = "TOP 1";

        public Selecter(string connectionString, ILoggerFactory loggerFactory) : base(connectionString, loggerFactory)
        {
        }

        protected override string FormatSql(QueryCommand command, WherePart wherePart)
        {
            return $"SELECT {(command.Single ? Top1 : string.Empty)} * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)}";
        }

        protected override ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters)
        {
            return new SqlCommandBuilder(connectionString, sql, parameters);
        }
    }
}