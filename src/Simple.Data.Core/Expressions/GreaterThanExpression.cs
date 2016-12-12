namespace Simple.Data.Core.Expressions
{
    public class GreaterThanExpression : IExpression, IBinaryExpression
    {
        public GreaterThanExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}