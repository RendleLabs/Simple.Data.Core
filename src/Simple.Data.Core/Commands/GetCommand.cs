using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class GetCommand : QueryCommandBase
    {
        public GetCommand(Wrangler wrangler, Table table, object[] key)
            : base(wrangler, table, true)
        {
            Key = key;
        }

        public IReadOnlyList<object> Key { get; }

        public override IExpression Criteria => new EqualsExpression(new ImplicitKey(), Key[0]);
    }
}