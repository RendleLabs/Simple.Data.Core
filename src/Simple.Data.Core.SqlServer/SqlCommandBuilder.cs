using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.SqlServer
{
    internal class SqlCommandBuilder : ICommandBuilder
    {
        private readonly string _connectionString;
        private readonly string _sql;
        private readonly IReadOnlyList<Parameter> _parameters;

        public SqlCommandBuilder(string connectionString, string sql, IReadOnlyList<Parameter> parameters)
        {
            _connectionString = connectionString;
            _sql = sql;
            _parameters = parameters;
        }

        public DbCommand Build()
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(_sql, connection);
            if (_parameters != null)
            {
                foreach (var parameter in _parameters)
                {
                    command.Parameters.AddWithValue("@" + parameter.Name, parameter.Value);
                }
            }
            return command;
        }
    }
}