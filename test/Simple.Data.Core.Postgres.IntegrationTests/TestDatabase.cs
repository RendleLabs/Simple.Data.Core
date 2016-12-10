using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Simple.Data.Core.Postgres.IntegrationTests
{
    public class TestDatabase : IDisposable
    {
        private readonly object _sync = new object();
        private readonly string _sqlFile;
        private readonly ILogger _logger;
        private string _connectionString;
        private string _databaseName;

        private TestDatabase(string sqlFile, ILoggerFactory loggerFactory)
        {
            _sqlFile = sqlFile;
            _logger = loggerFactory.CreateLogger<TestDatabase>();
        }

        public static TestDatabase Create(Type callingType, string sqlFile, ILoggerFactory loggerFactory)
        {
            return new TestDatabase($"{callingType.Namespace}.{sqlFile}", loggerFactory);
        }

        public Task<string> ConnectionString()
        {
            if (_connectionString == null)
            {
                lock (_sync)
                {
                    if (_connectionString == null)
                    {
                        return Create();
                    }
                }
            }
            return Task.FromResult(_connectionString);
        }

        private async Task<string> Create()
        {
            string sql;
            var assembly = typeof(TestDatabase).GetTypeInfo().Assembly;
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(_sqlFile)))
            {
                sql = await reader.ReadToEndAsync();
            }

            var dbName = await CreateDatabase();
            var connectionString = $"Server=localhost;Port=5432;Database={_databaseName};User Id=postgres;Password=s1mpl3;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    foreach (
                        var statement in
                        Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline)
                            .Where(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        //_logger.LogDebug(sql);
                        command.CommandText = statement;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            return _connectionString = connectionString;
        }

        private async Task<string> CreateDatabase()
        {
            _databaseName = $"simple_{Guid.NewGuid():N}".ToLowerInvariant();
            var connectionString = $"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=s1mpl3;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"CREATE DATABASE {_databaseName} WITH OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;";
                    _logger.LogDebug(cmd.CommandText);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(42999, ex, "Failed to CREATE DATABASE");
                    }
                }
            }
            return _databaseName;
        }

        public void Dispose()
        {
            if (_databaseName == null) return;
            try
            {
                var connectionString = $"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=s1mpl3;";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{_databaseName}';DROP DATABASE {_databaseName};";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(42999, ex, ex.Message);
            }
        }
    }
}