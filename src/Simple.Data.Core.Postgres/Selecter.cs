﻿using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Select;
using Simple.Data.Core.Sql.Where;

namespace Simple.Data.Core.Postgres
{
    public class Selecter : SelecterBase
    {
        private const string Limit1 = "LIMIT 1";
        public Selecter(string connectionString, ILoggerFactory loggerFactory) : base(connectionString, loggerFactory)
        {
        }

        protected override ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters)
        {
            return new PostgresCommandBuilder(connectionString, sql, parameters);
        }

        protected override string FormatSql(QueryCommand command, WherePart wherePart)
        {
            var limit = command.Single ? Limit1 : string.Empty;
            return $"SELECT * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)} {limit}";
        }
    }
}