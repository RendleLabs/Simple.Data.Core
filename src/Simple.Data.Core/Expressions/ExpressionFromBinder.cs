using System.Dynamic;
using System.Linq;

namespace Simple.Data.Core.Expressions
{
    public static class ExpressionFromBinder
    {
        public static IExpression Parse(Table table, object[] args, InvokeBinder binder)
        {
            var dict = ReadBinder.ParseArgs(args, binder);
            var kvp = dict.FirstOrDefault();
            var column = new Column(kvp.Key, table);
            if (kvp.Value is FloatingOperand)
            {
                return ((FloatingOperand) kvp.Value).GetExpression(column);
            }
            return SimpleExpression.Equal(column, kvp.Value);
        }
    }
}