using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.SqlServer
{
    public class SqlServerAdapter : Adapter
    {
        private readonly string _connectionString;
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();

        public SqlServerAdapter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override Task Execute(DataContext context)
        {
            // TODO: Replace with switch pattern when C# 7 is usable
            if (context.Request.Command is GetByCommand)
            {
                return ExecuteGetBy(context);
            }
            if (context.Request.Command is QueryCommandBase)
            {
                return ExecuteQuery(context);
            }
            throw new InvalidOperationException();
        }

        private async Task ExecuteQuery(DataContext context)
        {
            var command = (QueryCommandBase)context.Request.Command;
            var wherePart = _criteriaHelper.ToWherePart(command.Criteria);
            var sql = $"SELECT TOP 1 * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)}";
            var builder = new SqlCommandBuilder(_connectionString, sql, new[] {wherePart.Parameter});
            var run = new QueryRunner().Run(builder);
            if (command.Single)
            {
                context.Response.Result = await run.FirstOrDefault();
            }
            else
            {
                context.Response.Result = run;
            }
        }

        public override void Dispose()
        {
        }

        private async Task ExecuteGetBy(DataContext context)
        {
            var command = (GetByCommand)context.Request.Command;
            var wherePart = _criteriaHelper.ToWherePart(command.Criteria);
            var sql = $"SELECT TOP 1 * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)}";
            var builder = new SqlCommandBuilder(_connectionString, sql, new[] {wherePart.Parameter});
            context.Response.Result = await new QueryRunner().Run(builder).FirstOrDefault();
        }
    }

    public static class SqlFormatter
    {
        public static string FormatWherePart(WherePart wherePart)
        {
            return $"{Quote(wherePart.Column.QualifiedName)} {wherePart.Operator} @{wherePart.Parameter.Name}";
        }

        public static string Quote(LinkedList<string> name)
        {
            return "[" + string.Join("].[", name) + "]";
        }
    }
}
