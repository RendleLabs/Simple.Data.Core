namespace Simple.Data.Core.Expressions
{
    public class LessThanExpression : IExpression, IBinaryExpression
    {
        public LessThanExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}