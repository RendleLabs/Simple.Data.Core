using System;
using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class FindByCommand : QueryCommand
    {
        public FindByCommand(Wrangler wrangler, Table table, Column column, object value) : base(wrangler, table)
        {
            Column = column;
            Value = value;
        }

        public Column Column { get; }
        public object Value { get; }
        protected override Func<object, object> Finish
        {
            get { return o => new SimpleResultSet((IAsyncEnumerable<IReadOnlyDictionary<string, object>>) o); }
        }
        public override IExpression Criteria => SimpleExpression.Equal(Column, Value);
    }
}