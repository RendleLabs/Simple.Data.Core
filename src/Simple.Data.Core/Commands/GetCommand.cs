using System;
using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class GetCommand : QueryCommand
    {
        public GetCommand(Wrangler wrangler, Table table, IExpression criteria) : base(wrangler, table, true)
        {
            Criteria = criteria;
        }

        protected override Func<object, object> Finish => o => new SimpleRecord((IReadOnlyDictionary<string, object>) o);
        public override IExpression Criteria { get; }
    }
}