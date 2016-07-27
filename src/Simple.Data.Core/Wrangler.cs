using System.Dynamic;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core
{
    public class Wrangler
    {
        public bool Invoke(Thing thing, InvokeBinder binder, object[] args, out object result)
        {
            if (thing.Name == "Get")
            {
                var table = new Table(thing.Parent.Name);
                var column = new ImplicitKey(table);
                result = new Query(table, BinaryExpression.Equals(column, new ValueOperand<object>(args[0])), true);
                return true;
            }
            result = null;
            return false;
        }
    }
}