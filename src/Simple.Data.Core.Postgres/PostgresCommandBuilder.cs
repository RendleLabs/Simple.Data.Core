using System.Collections.Generic;
using System.Data.Common;
using Npgsql;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.Postgres
{
    internal class PostgresCommandBuilder : ICommandBuilder
    {
        private readonly string _connectionString;
        private readonly string _sql;
        private readonly IReadOnlyList<Parameter> _parameters;

        public PostgresCommandBuilder(string connectionString, string sql, IReadOnlyList<Parameter> parameters)
        {
            _connectionString = connectionString;
            _sql = sql;
            _parameters = parameters;
        }

        public DbCommand Build()
        {
            var connection = new NpgsqlConnection(_connectionString);
            var command = new NpgsqlCommand(_sql, connection);
            if (_parameters != null)
            {
                foreach (var parameter in _parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
            }
            return command;
        }
    }
}