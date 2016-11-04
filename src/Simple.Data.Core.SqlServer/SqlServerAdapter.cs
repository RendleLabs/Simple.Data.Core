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
        private ILogger<SqlServerAdapter> _logger;

        public SqlServerAdapter(string connectionString, ILoggerFactory loggerFactory)
        {
            _inserter = new Inserter(connectionString, loggerFactory);
            _selecter = new Selecter(connectionString, loggerFactory);
            _logger = loggerFactory.CreateLogger<SqlServerAdapter>();
        }

        public override Task Execute(DataContext context)
        {
            // TODO: Replace with switch pattern when C# 7 is usable
            if (context.Request.Command is QueryCommand)
            {
                _logger.LogDebug("Execute QueryCommand...");
                return _selecter.Execute(context);
            }
            if (context.Request.Command is InsertCommand)
            {
                return _inserter.Execute(context);
            }
            throw new InvalidOperationException();
        }

        public override void Dispose()
        {
        }
    }
}
