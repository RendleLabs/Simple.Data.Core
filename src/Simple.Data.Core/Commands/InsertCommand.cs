using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class InsertCommand: CommandBase
    {
        public InsertCommand(Wrangler wrangler, Table table, ImmutableDictionary<string, object> data) : base(wrangler)
        {
            Table = table;
            Data = data;
        }

        public Table Table { get; }
        public ImmutableDictionary<string, object> Data { get; }

        protected override Func<object, object> Finish => o => new SimpleRecord((IReadOnlyDictionary<string, object>) o);
    }
}