namespace Simple.Data.Core.Expressions
{
    public class EqualsExpression : IExpression
    {
        public EqualsExpression(object left, object right)
        {
            Left = left;
            Right = right;
        }

        public object Left { get; }
        public object Right { get; }
    }
}