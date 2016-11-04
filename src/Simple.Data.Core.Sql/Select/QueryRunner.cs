using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Sql.Select
{
    public class QueryRunner
    {
        private readonly ILoggerFactory _loggerFactory;

        public QueryRunner(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IAsyncEnumerable<IReadOnlyDictionary<string, object>> Run(ICommandBuilder builder)
        {
            return new DataReaderAsyncEnumerable(builder, _loggerFactory);
        }
    }
}
