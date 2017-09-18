using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;

namespace Simple.Data.Core.SqlServer
{
    public class SqlServerAdapter : Adapter
    {
        private readonly Inserter _inserter;
        private readonly Selecter _selecter;
        private readonly Updater _updater;
        private readonly ILogger<SqlServerAdapter> _logger;

        public SqlServerAdapter(string connectionString, ILoggerFactory loggerFactory)
        {
            _inserter = new Inserter(connectionString, loggerFactory);
            _selecter = new Selecter(connectionString, loggerFactory);
            _updater = new Updater(connectionString, loggerFactory);
            _logger = loggerFactory.CreateLogger<SqlServerAdapter>();
        }

        public override Task Execute(DataContext context)
        {
            switch (context.Request.Command)
            {
                case QueryCommand q:
                    return _selecter.Execute(context);
                case InsertCommand i:
                    return _inserter.Execute(context);
                case UpdateCommand u:
                    return _updater.Execute(context);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
