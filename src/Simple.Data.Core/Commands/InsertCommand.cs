using System;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class InsertCommand: CommandBase
    {
        public InsertCommand(Wrangler wrangler, Table table, object data) : base(wrangler)
        {
            Table = table;
            Data = data;
        }

        public Table Table { get; }
        public object Data { get; }

        protected override Func<object, object> Finish { get; }
    }
}