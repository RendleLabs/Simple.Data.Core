using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql.Where;
using System.Linq;
using Simple.Data.Core.Sql.Select;

namespace Simple.Data.Core.Sql.Update
{
    public abstract class UpdaterBase
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();
        private readonly QueryRunner _queryRunner;

        protected UpdaterBase(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _logger = loggerFactory.CreateLogger<UpdaterBase>();
            _queryRunner = new QueryRunner(loggerFactory);
        }

        public async Task Execute(DataContext context)
        {
            var command = (UpdateCommand) context.Request.Command;
            var update = UpdateStatement.Create(command.Table, command.Data, context);
            var wherePart = _criteriaHelper.ToWherePart(command.Criteria);
            var sql = FormatSql(update, wherePart);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(sql);
            }
            var builder = CreateBuilder(_connectionString, sql, new[] { wherePart.Parameter }.Concat(update.Values).ToArray());
            context.Response.Result = await _queryRunner.Run(builder).FirstOrDefault();
        }

        protected abstract ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters);

        protected abstract string FormatSql(UpdateStatement command, WherePart wherePart);
    }
}