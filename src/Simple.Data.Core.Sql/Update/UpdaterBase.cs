using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql.Where;
using System.Linq;

namespace Simple.Data.Core.Sql.Update
{
    public abstract class UpdaterBase
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();

        protected UpdaterBase(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _logger = loggerFactory.CreateLogger<UpdaterBase>();
        }

        public async Task Execute(DataContext context)
        {
            var command = (QueryCommand) context.Request.Command;
            var wherePart = _criteriaHelper.ToWherePart(command.Criteria);
            var sql = FormatSql(command, wherePart);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(sql);
            }
            var builder = CreateBuilder(_connectionString, sql, new[] {wherePart.Parameter});
            var run = _queryRunner.Run(builder);
            if (command.Single)
            {
                context.Response.Result = await run.FirstOrDefault();
            }
            else
            {
                context.Response.Result = run;
            }
        }

        protected abstract ICommandBuilder CreateBuilder(string connectionString, string sql, Parameter[] parameters);

        protected abstract string FormatSql(QueryCommand command, WherePart wherePart);
    }
}