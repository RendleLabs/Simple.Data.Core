using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public abstract class QueryCommandBase : CommandBase
    {
        protected QueryCommandBase(Wrangler wrangler, Table table, bool single = false) : base(wrangler)
        {
            Table = table;
            Single = single;
        }

        public Table Table { get; }

        public abstract IExpression Criteria { get; }

        public bool Single { get; }
    }
}