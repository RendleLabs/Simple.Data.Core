using System;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class FindCommand : QueryCommand
    {
        public FindCommand(Wrangler wrangler, Table table, IExpression criteria) : base(wrangler, table)
        {
            Criteria = criteria;
        }

        protected override Func<object, object> Finish => ToSimpleResultSet;
        public override IExpression Criteria { get; }
    }
}