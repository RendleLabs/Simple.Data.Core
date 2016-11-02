using System;
using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class GetByCommand : QueryCommandBase 
    {
        public GetByCommand(Wrangler wrangler, Table table, Column column, object value) : base(wrangler, table, true)
        {
            Column = column;
            Value = value;
        }

        public Column Column { get; }
        public object Value { get; }
        public override IExpression Criteria => SimpleExpression.Equal(Column, Value);

        protected override Func<object, object> Finish
        {
            get { return o => new SimpleRecord((IReadOnlyDictionary<string, object>) o); }
        }
    }

    public class FindByCommand : QueryCommandBase
    {
        public FindByCommand(Wrangler wrangler, Table table) : base(wrangler, table, true)
        {
        }

        protected override Func<object, object> Finish { get; }
        public override IExpression Criteria { get; }
    }
}