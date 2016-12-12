using System.Reflection.Emit;

namespace Simple.Data.Core.Expressions
{
    public static class SimpleExpression
    {
        public static EqualExpression Equal(object left, object right)
        {
            return new EqualExpression(left, right);
        }

        public static NotEqualExpression NotEqual(object left, object right)
        {
            return new NotEqualExpression(left, right);
        }

        public static GreaterThanExpression GreaterThan(object left, object right)
        {
            return new GreaterThanExpression(left, right);
        }

        public static GreaterThanExpression GreaterThanOrEqual(object left, object right)
        {
            return new GreaterThanExpression(left, right);
        }

        public static LessThanExpression LessThan(object left, object right)
        {
            return new LessThanExpression(left, right);
        }

        public static LessThanExpression LessThanOrEqual(object left, object right)
        {
            return new LessThanExpression(left, right);
        }
    }
}