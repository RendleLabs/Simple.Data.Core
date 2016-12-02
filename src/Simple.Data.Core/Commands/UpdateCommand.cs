using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class UpdateCommand : CommandBase
    {
        public UpdateCommand(Wrangler wrangler, Table table, IExpression criteria, ImmutableDictionary<string, object> data) : base(wrangler)
        {
            Table = table;
            Data = data;
            Criteria = criteria;
        }

        public Table Table { get; }
        public ImmutableDictionary<string, object> Data { get; }
        public IExpression Criteria { get; }
        protected override Func<object, object> Finish => o => new SimpleRecord((IReadOnlyDictionary<string, object>) o);
    }
}
