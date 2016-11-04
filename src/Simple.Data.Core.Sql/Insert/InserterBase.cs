using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql.Select;
using System.Linq;

namespace Simple.Data.Core.Sql.Insert
{
    public abstract class InserterBase
    {
        private readonly QueryRunner _queryRunner;
        private readonly string _connectionString;
        private readonly ILogger<InserterBase> _logger;

        protected InserterBase(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _logger = loggerFactory.CreateLogger<InserterBase>();
            _queryRunner = new QueryRunner(loggerFactory);
        }

        public async Task Execute(DataContext context)
        {
            var command = (InsertCommand) context.Request.Command;
            var insert = InsertStatement.Create(command.Table, command.Data, context);
            var sql = FormatSql(insert);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(sql);
            }
            var builder = CreateBuilder(_connectionString, sql, insert.Values);
            context.Response.Result = await _queryRunner.Run(builder).FirstOrDefault();
        }

        protected abstract string FormatSql(InsertStatement insert);
        protected abstract ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters);
    }
}