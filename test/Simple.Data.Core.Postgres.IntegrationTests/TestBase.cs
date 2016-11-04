using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Postgres.IntegrationTests
{
    public abstract class TestBase : IDisposable
    {
        private readonly SemaphoreSlim _sync = new SemaphoreSlim(1);
        private readonly ILoggerFactory _loggerFactory = new LoggerFactory().AddConsole(LogLevel.Debug);
        private TestDatabase _database;

        protected async Task<dynamic> Target()
        {
            if (_database == null)
            {
                await _sync.WaitAsync();
                if (_database == null)
                {
                    try
                    {
                        _database = TestDatabase.Create(this.GetType(), SqlFileName, _loggerFactory);
                    }
                    finally
                    {
                        _sync.Release();
                    }
                }
            }
            return new SimpleData(_loggerFactory).Open(await _database.ConnectionString(), typeof(PostgresAdapter));
        }

        protected abstract string SqlFileName { get; }
        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}