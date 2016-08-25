using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Data.Core.Sql
{
    internal class DataReaderAsyncEnumerator : IAsyncEnumerator<IReadOnlyDictionary<string, object>>
    {
        private readonly DbConnection _connection;
        private readonly bool _disposeConnection;
        private readonly DbCommand _command;
        private DbDataReader _reader;
        private IReadOnlyDictionary<string, int> _keyIndexes;
        public DataReaderAsyncEnumerator(DbCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (command.Connection == null) throw new ArgumentException("DbCommand.Connection not set.", nameof(command));
            _command = command;
            _connection = command.Connection;
            _disposeConnection = _connection.State == ConnectionState.Closed;
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
                await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                _reader = await _command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                _keyIndexes = DataRecord.CreateIndex(_reader);
            }
            return await _reader.ReadAsync(cancellationToken).ConfigureAwait(false);
        }

        public IReadOnlyDictionary<string, object> Current => DataRecord.FromDataReader(_keyIndexes, _reader);
    }
}