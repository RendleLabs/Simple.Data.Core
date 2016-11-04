using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Sql
{
    internal class DataReaderAsyncEnumerator : IAsyncEnumerator<IReadOnlyDictionary<string, object>>
    {
        private readonly DbConnection _connection;
        private readonly bool _disposeConnection;
        private readonly DbCommand _command;
        private DbDataReader _reader;
        private IReadOnlyDictionary<string, int> _keyIndexes;
        private readonly ILogger<DataReaderAsyncEnumerator> _logger;

        public DataReaderAsyncEnumerator(DbCommand command, ILoggerFactory loggerFactory)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (command.Connection == null) throw new ArgumentException("DbCommand.Connection not set.", nameof(command));
            _command = command;
            _connection = command.Connection;
            _disposeConnection = _connection.State == ConnectionState.Closed;
            _logger = loggerFactory.CreateLogger<DataReaderAsyncEnumerator>();
        }

        public void Dispose()
        {
            _reader?.Dispose();
            _command.Dispose();
            if (_disposeConnection)
            {
                _connection.Dispose();
            }
        }

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            if (_reader == null)
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (DbException ex)
                    {
                        _logger.LogError(EventIds.ConnectError, ex, "DbConnection.OpenAsync threw exception.");
                        throw;
                    }
                }
                try
                {
                    _reader = await _command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                }
                catch (DbException ex)
                {
                    _logger.LogError(EventIds.ExecuteError, ex, "DbCommand.ExecuteReaderAsync threw exception with SQL '{sql}'", _command.CommandText);
                    throw;
                }
                _keyIndexes = DataRecord.CreateIndex(_reader);
            }
            try
            {
                if (!await _reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    return false;
                }
                return true;
            }
            catch (DbException ex)
            {
                _logger.LogError(EventIds.ExecuteError, ex, "DbDataReader.ReadAsync threw exception with SQL '{sql}'", _command.CommandText);
                throw;
            }
        }

        public IReadOnlyDictionary<string, object> Current => DataRecord.FromDataReader(_keyIndexes, _reader);
    }
}