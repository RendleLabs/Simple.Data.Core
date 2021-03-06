using System;
using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class GetByCommand : QueryCommand 
    {
        public GetByCommand(Wrangler wrangler, Table table, Column column, object value) : base(wrangler, table, true)
        {
            Column = column;
            Value = value;
        }

        public Column Column { get; }
        public object Value { get; }
        public override IExpression Criteria => SimpleExpression.Equal(Column, Value);

        protected override Func<object, object> Finish => o => new SimpleRecord((IReadOnlyDictionary<string, object>) o);
    }
}