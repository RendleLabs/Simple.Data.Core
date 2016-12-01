using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public abstract class UpdateCommand : CommandBase
    {
        private readonly Wrangler _wrangler;
        protected UpdateCommand(Wrangler wrangler, Table table, ImmutableDictionary<string, object> data, IExpression criteria, bool single = false) : base(wrangler)
        {
            _wrangler = wrangler;
            Table = table;
            Data = data;
            Criteria = criteria;
            Single = single;
        }

        public Table Table { get; }

        public ImmutableDictionary<string, object> Data { get; }
        public IExpression Criteria { get; }

        public bool Single { get; }

        public async Task<List<dynamic>> ToList()
        {
            if (Single) throw new InvalidOperationException("Single Update cannot produce a List.");
            var resultSet = await _wrangler.Execute(this, ToSimpleResultSet);
            return await resultSet.ToList();
        }

        protected SimpleResultSet ToSimpleResultSet(object o)
        {
            return new SimpleResultSet((IAsyncEnumerable<IReadOnlyDictionary<string, object>>) o);
        }
    }
}
