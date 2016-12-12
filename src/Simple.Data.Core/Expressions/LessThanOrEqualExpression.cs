namespace Simple.Data.Core.Expressions
{
    public class LessThanOrEqualExpression : IExpression, IBinaryExpression
    {
        public LessThanOrEqualExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}