namespace Simple.Data.Core.Expressions
{
    public class EqualExpression : IExpression, IBinaryExpression
    {
        public EqualExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}