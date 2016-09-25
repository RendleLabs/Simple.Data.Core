using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.Postgres
{
    public class PostgresAdapter : Adapter
    {
        private readonly string _connectionString;
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();

        public PostgresAdapter(string connectionString)
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
            throw new InvalidOperationException();
        }

        public override void Dispose()
        {
        }

        private async Task ExecuteGetBy(DataContext context)
        {
            var command = (GetByCommand)context.Request.Command;
            var wherePart = _criteriaHelper.ToWherePart(command.Criteria);
            var sql = $"SELECT * FROM {QuoteHelper.Quote(command.Table.QualifiedName)} WHERE {SqlFormatter.FormatWherePart(wherePart)} LIMIT 1";
            Console.WriteLine(sql);
            var builder = new PostgresCommandBuilder(_connectionString, sql, new[] {wherePart.Parameter});
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
            return string.Join(".", name);
        }
    }
}
