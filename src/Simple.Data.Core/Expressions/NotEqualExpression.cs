namespace Simple.Data.Core.Expressions
{
    public class NotEqualExpression : IExpression, IBinaryExpression
    {
        public NotEqualExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}