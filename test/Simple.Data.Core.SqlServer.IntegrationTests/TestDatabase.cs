using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.SqlServer.IntegrationTests
{
    public class TestDatabase : IDisposable
    {
        private readonly object _sync = new object();
        private readonly string _sqlFile;
        private readonly ILogger _logger;
        private string _connectionString;
        private string _databaseName;
        private string _fileName;

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

            try
            {
                Directory.CreateDirectory("temp");
            }
            catch
            {
            }

            var dbName = await CreateDatabase();
            var connectionString = $@"Server=(localdb)\mssqllocaldb;Database={dbName};Trusted_Connection=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    foreach (
                        var statement in
                        Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline)
                            .Where(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        command.CommandText = statement;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            return _connectionString = connectionString;
        }

        private async Task<string> CreateDatabase()
        {
            var pwd = Directory.GetCurrentDirectory();
            _databaseName = $"Simple_{Guid.NewGuid():N}";
            _fileName = $@"{pwd}\temp\{_databaseName}";
            using (var connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb"))
            {
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $@"CREATE DATABASE [{_databaseName}] 
ON PRIMARY (NAME={_databaseName}_data, FILENAME='{_fileName}_data.mdf')
LOG ON (NAME={_databaseName}_log, FILENAME='{_fileName}_log.ldf')";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return _databaseName;
        }

        public void Dispose()
        {
            if (_databaseName == null) return;
            try
            {
                using (var connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb"))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                            $"ALTER DATABASE [{_databaseName}] SET OFFLINE WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_databaseName}];";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(42999, ex, ex.Message);
            }
            try
            {
                File.Delete($"{_fileName}_data.mdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(42999, ex, ex.Message);
            }
            try
            {
                File.Delete($"{_fileName}_log.ldf");
            }
            catch (Exception ex)
            {
                _logger.LogError(42999, ex, ex.Message);
            }
        }
    }
}