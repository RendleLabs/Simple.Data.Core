using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public abstract class QueryCommand : CommandBase
    {
        private readonly Wrangler _wrangler;
        protected QueryCommand(Wrangler wrangler, Table table, bool single = false) : base(wrangler)
        {
            _wrangler = wrangler;
            Table = table;
            Single = single;
        }

        public Table Table { get; }

        public abstract IExpression Criteria { get; }

        public bool Single { get; }

        public async Task<List<dynamic>> ToList()
        {
            if (Single) throw new InvalidOperationException("Single Query cannot produce a List.");
            var resultSet = await _wrangler.Execute(this, ToSimpleResultSet);
            return await resultSet.ToList();
        }

        protected SimpleResultSet ToSimpleResultSet(object o)
        {
            return new SimpleResultSet((IAsyncEnumerable<IReadOnlyDictionary<string, object>>) o);
        }
    }
}