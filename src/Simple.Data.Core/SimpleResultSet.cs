using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public class SimpleResultSet: IAsyncEnumerable<SimpleRecord>
    {
        private readonly IAsyncEnumerable<IReadOnlyDictionary<string, object>> _source;

        public SimpleResultSet(IAsyncEnumerable<IReadOnlyDictionary<string, object>> source)
        {
            _source = source;
        }

        public async Task<dynamic> FirstOrDefaultAsync(CancellationToken token)
        {
            var first = await _source.FirstOrDefault(token);
            return first == null ? null : new SimpleRecord(first);
        }

        public Task ForEachAsync(Action<dynamic> action, CancellationToken token = default(CancellationToken))
        {
            return _source.ForEachAsync(dict =>
            {
                action(new SimpleRecord(dict));
            }, token);
        }

        public Task<dynamic[]> ToArray()
        {
            return _source.Select(dict => (dynamic)new SimpleRecord(dict)).ToArray();
        }

        public Task<List<dynamic>> ToList()
        {
            return _source.Select(dict => (dynamic)new SimpleRecord(dict)).ToList();
        }

        public IAsyncEnumerator<SimpleRecord> GetEnumerator()
        {
            return _source.Select(s => new SimpleRecord(s)).GetEnumerator();
        }
    }
}