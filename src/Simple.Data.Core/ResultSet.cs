using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public class ResultSet
    {
        private readonly IAsyncEnumerable<IReadOnlyDictionary<string, object>> _source;

        public ResultSet(IAsyncEnumerable<IReadOnlyDictionary<string, object>> source)
        {
            _source = source;
        }

        public async Task<dynamic> FirstOrDefaultAsync(CancellationToken token)
        {
            var first = await _source.FirstOrDefault(token);
            return first == null ? null : new SimpleRecord(first);
        }
    }
}