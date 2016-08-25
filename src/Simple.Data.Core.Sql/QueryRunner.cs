using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Simple.Data.Core.Sql
{
    public class QueryRunner
    {
        public IAsyncEnumerable<IReadOnlyDictionary<string, object>> Run(ICommandBuilder builder)
        {
            return new DataReaderAsyncEnumerable(builder);
        }
    }

    public interface ICommandBuilder
    {
        DbCommand Build();
    }

    internal class DataReaderAsyncEnumerable : IAsyncEnumerable<IReadOnlyDictionary<string,object>>
    {
        private readonly ICommandBuilder _builder;

        public DataReaderAsyncEnumerable(ICommandBuilder builder)
        {
            _builder = builder;
        }

        public IAsyncEnumerator<IReadOnlyDictionary<string, object>> GetEnumerator()
        {
            return new DataReaderAsyncEnumerator(_builder.Build());
        }
    }
}
