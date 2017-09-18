using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Select;
using Simple.Data.Core.Sql.Where;

namespace Simple.Data.Core.Postgres
{
    public class PostgresAdapter : Adapter
    {
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();
        private readonly Inserter _inserter;
        private readonly Selecter _selecter;
        private readonly Updater _updater;

        public PostgresAdapter(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _loggerFactory = loggerFactory;
            _inserter = new Inserter(connectionString, loggerFactory);
            _selecter = new Selecter(connectionString, loggerFactory);
            _updater = new Updater(connectionString, loggerFactory);
        }

        public override Task Execute(DataContext context)
        {
            // TODO: Replace with switch pattern when C# 7 is usable
            if (context.Request.Command is QueryCommand)
            {
                return _selecter.Execute(context);
            }
            if (context.Request.Command is InsertCommand)
            {
                return _inserter.Execute(context);
            }
            if (context.Request.Command is UpdateCommand)
            {
                return _updater.Execute(context);
            }
            throw new InvalidOperationException();
        }
    }
}
