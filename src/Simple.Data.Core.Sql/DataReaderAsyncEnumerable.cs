using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Sql
{
    internal class DataReaderAsyncEnumerable : IAsyncEnumerable<IReadOnlyDictionary<string, object>>
    {
        private readonly ICommandBuilder _builder;
        private readonly ILoggerFactory _loggerFactory;

        public DataReaderAsyncEnumerable(ICommandBuilder builder, ILoggerFactory loggerFactory)
        {
            _builder = builder;
            _loggerFactory = loggerFactory;
        }

        public IAsyncEnumerator<IReadOnlyDictionary<string, object>> GetEnumerator()
        {
            return new DataReaderAsyncEnumerator(_builder.Build(), _loggerFactory);
        }
    }
}