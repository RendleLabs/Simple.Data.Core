﻿using Microsoft.Extensions.Logging;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Insert;
using Simple.Data.Core.Sql.Select;

namespace Simple.Data.Core.SqlServer
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
            return new SqlCommandBuilder(connectionString, sql, parameters);
        }
    }
}
