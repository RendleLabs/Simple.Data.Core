namespace Simple.Data.Core.Expressions
{
    public class GreaterThanOrEqualExpression : IExpression, IBinaryExpression
    {
        public GreaterThanOrEqualExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}