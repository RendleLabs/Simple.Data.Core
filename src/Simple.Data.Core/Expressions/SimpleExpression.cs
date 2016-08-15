using System.Reflection.Emit;

namespace Simple.Data.Core.Expressions
{
    public static class SimpleExpression
    {
        public static EqualsExpression Equal(object left, object right)
        {
            return new EqualsExpression(left, right);
        }
    }
}